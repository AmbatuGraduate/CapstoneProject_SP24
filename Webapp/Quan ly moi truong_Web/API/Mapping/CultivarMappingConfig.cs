using Application.Cultivar.Common;
using Application.Cultivar.Queries.GetById;
using Application.Tree.Common;
using Contract.Cultival;
using Contract.Tree;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
