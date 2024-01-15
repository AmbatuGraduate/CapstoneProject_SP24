using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Tree
{
    public record AddTreeRequest
    (
        Guid id, 
        string district, 
        string street,
        string rootType, 
        string type, 
        float bodyDiameter,
        float leafLength, 
        DateTime plantTime, 
        DateTime cutTime,
        int intervalCutTime, 
        string note 
    );
}
