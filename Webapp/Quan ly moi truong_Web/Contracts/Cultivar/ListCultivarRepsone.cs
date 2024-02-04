using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Cultivar
{
    public record ListCultivarRepsone
    (
        string CultivarName,
        Guid TreeTypeId
    );
}
