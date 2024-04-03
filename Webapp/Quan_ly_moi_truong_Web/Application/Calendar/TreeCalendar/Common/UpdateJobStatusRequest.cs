using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Calendar.TreeCalendar.Common
{
    public class UpdateJobStatusRequest
    {
        public JobWorkingStatus jobWorkingStatus {  get; set; }
        public string eventId { get; set; }
    }
}
