using Application.Tree.Commands.Add;
using Application.Tree.Commands.Delete;
using Application.Tree.Commands.Update;
using Application.Tree.Common;
using Application.Tree.Queries.GetById;
using Application.Tree.Queries.GetByTreeCode;
using Application.Tree.Queries.ListCut;
using Contract.Tree;
using Mapster;

namespace API.Mapping
{
    // Update At: 17/01/2024
    // Update By: Dang Nguyen Khanh Vu
    // Change:
    // - Sửa lại mapping của GetByTreeCodeQuery và DeleteTreeCommand
    // -> Lý do: Lỗi không thể cast kiểu string sang class type -> do không thể casting primitive type sang class type
    // -> Cách sửa: Thay vì dùng 'cofig.NewConfig().Map()' thành 'cofig.NewConfig().MapWith()'
    // *****
    // - Sửa lại mapping của TreeResult với ListTreeResponse
    // -> Lý do: casting giữa TreeResult với ListTreeResponse trả về rỗng
    // -> Cách sửa: Map từng property của TreeResult với ListTreeResponse
    // ****
    // - Thêm mapping của TreeResult với DetailTreeResponse

    public class TreeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(string, UpdateTreeRequest), UpdateTreeCommand>()
                .MapWith(dest => new UpdateTreeCommand(
                    dest.Item1,
                    dest.Item2.TreeLocation, dest.Item2.BodyDiameter,
                    dest.Item2.LeafLength, dest.Item2.PlantTime,
                    dest.Item2.IntervalCutTime, dest.Item2.TreeTypeId,
                    dest.Item2.Note, dest.Item2.Email));

            config.NewConfig<AddTreeRequest, AddTreeCommand>();

            config.NewConfig<string, DeleteTreeCommand>()
                .MapWith(dest => new DeleteTreeCommand(dest));

            config.NewConfig<string, GetByTreeCodeQuery>()
                .MapWith(dest => new GetByTreeCodeQuery(dest));


            config.NewConfig<string, ListTreeCutQuery>()
                .MapWith(dest => new ListTreeCutQuery(dest));

            config.NewConfig<Guid, GetByIdQuery>()
                .Map(dest => dest.TreeId, src => src);


            config.NewConfig<TreeResult, ListTreeResponse>()
                .Map(dest => dest.TreeCode, src => src.TreeCode)
                .Map(dest => dest.StreetName, src => src.StreetName)
                .Map(dest => dest.BodyDiameter, src => src.BodyDiameter)
                .Map(dest => dest.LeafLength, src => src.LeafLength)
                .Map(dest => dest.CutTime, src => src.CutTime)
                .Map(dest => dest.TreeType, src => src.TreeType)
                .Map(dest => dest.isCut, src => src.isCut);

            config.NewConfig<TreeDetailResult, DetailTreeResponse>()
                .Map(dest => dest.TreeCode, src => src.TreeCode)
                .Map(dest => dest.StreetName, src => src.StreetName)
                .Map(dest => dest.BodyDiameter, src => src.BodyDiameter)
                .Map(dest => dest.LeafLength, src => src.LeafLength)
                .Map(dest => dest.PlantTime, src => src.PlantTime)
                .Map(dest => dest.CutTime, src => src.CutTime)
                .Map(dest => dest.IntervalCutTime, src => src.IntervalCutTime)
                .Map(dest => dest.isCut, src => src.isCut)
                .Map(dest => dest.User, src => src.User)
                .Map(dest => dest.Note, src => src.Note);

            config.NewConfig<AddTreeResult, AddTreeResponse>()
                  .Map(dest => dest.TreeCode, src => src.TreeCode);
        }
    }
}