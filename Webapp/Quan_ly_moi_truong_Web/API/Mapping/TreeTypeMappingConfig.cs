using Application.TreeType.Commands.Add;
using Application.TreeType.Commands.Update;
using Application.TreeType.Common;
using Application.TreeType.Queries.GetById;
using Contract.TreeType;
using Mapster;

namespace API.Mapping
{
    public class TreeTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddTreeTypeRequest, AddTreeTypeCommand>();

            config.NewConfig<Guid, GetByIdQuery>()
                  .Map(dest => dest.TreeTypeId, src => src);

            config.NewConfig<(Guid, UpdateTreeTypeRequest), UpdateTreeTypeCommand>()
                  .MapWith(dest => new UpdateTreeTypeCommand(dest.Item1, dest.Item2.TreeTypeName, dest.Item2.UpdateBy));

            config.NewConfig<TreeTypeResult, ListTreeTypeResponse>()
                  .Map(dest => dest.TreeTypeName, src => src.treeTypeResults.TreeTypeName)
                  .Map(dest => dest.TreeTypeId, src => src.treeTypeResults.TreeTypeId);
        }
    }
}
