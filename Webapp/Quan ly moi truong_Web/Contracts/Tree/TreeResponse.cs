using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Tree
{
    public record TreeResponse
    (
        int Id, string District, string Street,
        string RootType, string Type, float BodyDiameter,
        float LeafLength, DateTime PlantTime, DateTime CutTime,
        int IntervalCutTime, string Note
    );
}
