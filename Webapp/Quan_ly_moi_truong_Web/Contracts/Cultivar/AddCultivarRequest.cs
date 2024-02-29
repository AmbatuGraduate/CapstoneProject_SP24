using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Cultivar
{
    public record AddCultivarRequest
    (
        string CultivarName,
        Guid TreeTypeId,
        string CreateBy,
        string UpdateBy
    );
}
