using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Tree
{
    public record ListTreeResponse
    (
        string TreeCode,
        Guid StreetId,
        DateTime PlantTime,
        DateTime CutTime,
        Guid CultivarId,
        bool isExist
    );
}
