using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tree.Common
{
    public record TreeDetailResult(
        string TreeCode,
        string StreetName,
        string Cultivar,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        DateTime CutTime,
        string Note
    );
}
