using Application.Cultivar.Commands.Add;
using Application.Cultivar.Commands.Update;
using Application.Cultivar.Common;
using Application.Cultivar.Queries.GetById;
using Contract.Cultivar;
using Mapster;

namespace API.Mapping
{
    public class CultivalMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddCultivarRequest, AddCultivarCommand>();

            config.NewConfig<(Guid, UpdateCultivarRequest), UpdateCultivarCommand>()
                  .MapWith(dest => new UpdateCultivarCommand(dest.Item1, dest.Item2.CultivarName, dest.Item2.TreeTypeId, dest.Item2.UpdateBy));

            config.NewConfig<Guid, GetByIdQuery>()
                  .Map(dest => dest.CultivarId, src => src);

            config.NewConfig<CultivarResult, ListCultivarRepsone>()
                .Map(dest => dest.CultivarName, src => src.cultivars.CultivarName)
                .Map(dest => dest.TreeTypeId, src => src.cultivars.TreeTypeId);
        }
    }
}