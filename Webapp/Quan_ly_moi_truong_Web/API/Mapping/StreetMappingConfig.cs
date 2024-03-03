using Application.Street.Commands.Add;
using Application.Street.Commands.Delete;
using Application.Street.Commands.Update;
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

            // config for get all
            config.NewConfig<StreetResult, ListStreetResponse>()
                .Map(dest => dest.StreetId, src => src.street.StreetId)
                .Map(dest => dest.StreetName, src => src.street.StreetName)
                .Map(dest => dest.StreetLength, src => src.street.StreetLength)
                .Map(dest => dest.NumberOfHouses, src => src.street.NumberOfHouses)
                .Map(dest => dest.StreetTypeId, src => src.street.StreetTypeId)
                .Map(dest => dest.WardId, src => src.street.WardId);

            // config for get by id
            config.NewConfig<StreetResult, DetailsStreetResponse>()
                .Map(dest => dest, src => src.street);

            // config for add
            config.NewConfig<AddStreetRequest, AddStreetCommand>();

            // config for update
            config.NewConfig<(string, UpdateStreetRequest), UpdateStreetCommand>()
                .MapWith(dest => new UpdateStreetCommand(
                                       new Guid (dest.Item1),
                                       dest.Item2.StreetName, 
                                       dest.Item2.StreetLength,
                                       dest.Item2.NumberOfHouses, 
                                       dest.Item2.StreetTypeId,
                                       dest.Item2.WardId,
                                       dest.Item2.UpdateBy));
            // config for delete
            config.NewConfig<Guid, DeleteStreetCommand>()
                .MapWith(dest => new DeleteStreetCommand(dest));
        }   
    }
}
