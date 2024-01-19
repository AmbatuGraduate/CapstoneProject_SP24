using Application.Tree.Commands.Add;
using Application.Tree.Commands.Delete;
using Application.Tree.Commands.Update;
using Application.Tree.Common;
using Application.Tree.Queries.GetById;
using Application.Tree.Queries.GetByTreeCode;
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
                    dest.Item2.StreetId, dest.Item2.BodyDiameter,
                    dest.Item2.LeafLength, dest.Item2.PlantTime,
                    dest.Item2.IntervalCutTime, dest.Item2.CultivarId,
                    dest.Item2.Note, dest.Item2.UpdateBy,
                    dest.Item2.isExist));

            config.NewConfig<AddTreeRequest, AddTreeCommand>();

            config.NewConfig<string, DeleteTreeCommand>()
                .MapWith(dest => new DeleteTreeCommand(dest));

            config.NewConfig<string, GetByTreeCodeQuery>()
                .MapWith(dest => new GetByTreeCodeQuery(dest));

            config.NewConfig<Guid, GetByIdQuery>()
                .Map(dest => dest.TreeId, src => src);

            config.NewConfig<TreeResult, ListTreeResponse>()
                .Map(dest => dest.TreeCode, src => src.tree.TreeCode)
                .Map(dest => dest.StreetId, src => src.tree.StreetId)
                .Map(dest => dest.PlantTime, src => src.tree.PlantTime)
                .Map(dest => dest.CutTime, src => src.tree.CutTime)
                .Map(dest => dest.CultivarId, src => src.tree.CultivarId)
                .Map(dest => dest.isExist, src => src.tree.isExist);

            config.NewConfig<TreeResult, DetailTreeResponse>()
                .Map(dest => dest, src => src.tree);
        }
    }
}