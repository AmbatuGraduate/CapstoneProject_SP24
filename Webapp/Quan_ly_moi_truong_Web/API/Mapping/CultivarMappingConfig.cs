using Application.Cultivar.Common;
using Application.Cultivar.Queries.GetById;
using Contract.Cultivar;
using Mapster;

namespace API.Mapping
{
    public class CultivarMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetByIdQuery>()
                  .Map(dest => dest.CultivarId, src => src);

            config.NewConfig<CultivarResult, ListCultivarRepsone>()
                .Map(dest => dest.CultivarName, src => src.cultivars.CultivarName)
                .Map(dest => dest.TreeTypeId, src => src.cultivars.TreeTypeId);
        }
    }
}