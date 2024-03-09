using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum JobWorkingStatus
    {
        None = -1,
        NotStart = 0,
        InProgress = 1,
        Done = 2,
        DoneWithIssue = 3
    }
}
