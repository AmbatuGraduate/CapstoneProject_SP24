using Application.Tree.Commands.Add;
using Application.Tree.Commands.Delete;
using Application.Tree.Commands.Update;
using Application.Tree.Common;
using Application.Tree.Queries.GetById;
using Contract.Tree;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Mapping
{
    public class TreeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(int, UpdateTreeRequest), UpdateTreeCommand>()
                .Map(dest => dest.id, src => src.Item1)
                .Map(dest => dest.district, src => src.Item2.district)
                .Map(dest => dest.street, src => src.Item2.street)
                .Map(dest => dest.rootType, src => src.Item2.rootType)
                .Map(dest => dest.type, src => src.Item2.type)
                .Map(dest => dest.bodyDiameter, src => src.Item2.bodyDiameter)
                .Map(dest => dest.leafLength, src => src.Item2.leafLength)
                .Map(dest => dest.plantTime, src => src.Item2.plantTime)
                .Map(dest => dest.cutTime, src => src.Item2.cutTime)
                .Map(dest => dest.intervalCutTime, src => src.Item2.intervalCutTime)
                .Map(dest => dest.note, src => src.Item2.note);

            config.NewConfig<AddTreeRequest, AddTreeCommand>();

            config.NewConfig<int, DeleteTreeCommand>()
                .Map(dest => dest.id, src => src);

            config.NewConfig<TreeResult, TreeResponse>()
                .Map(dest => dest, src => src.tree);
        }
    }
}
