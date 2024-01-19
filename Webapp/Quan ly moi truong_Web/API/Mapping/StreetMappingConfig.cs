using Application.Street.Common;
using Application.Street.Queries.GetById;
using Contract.Street;
using Mapster;

namespace API.Mapping
{
    public class StreetMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<Guid, GetByIdQuery>()
                .Map(dest => dest.StreetId, src => src);


            config.NewConfig<StreetResult, ListStreetResponse>()
                .Map(dest => dest.StreetId, src => src.street.StreetId)
                .Map(dest => dest.StreetName, src => src.street.StreetName)
                .Map(dest => dest.StreetLength, src => src.street.StreetLength)
                .Map(dest => dest.NumberOfHouses, src => src.street.NumberOfHouses)
                .Map(dest => dest.StreetTypeId, src => src.street.StreetTypeId)
                .Map(dest => dest.WardId, src => src.street.WardId);


        }   
    }
}
