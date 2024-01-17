using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Tree
{
    public record UpdateTreeRequest
    (
        Guid StreetId,
        float BodyDiameter,
        float LeafLength,
        DateTime PlantTime,
        int IntervalCutTime,
        Guid CultivarId,
        string Note,
        string UpdateBy,
        bool isExist
    );
}
