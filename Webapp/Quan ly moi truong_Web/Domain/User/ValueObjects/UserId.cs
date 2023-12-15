using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User.ValueObjects
{

    /// <summary>
    /// It's just for example implement AggregateRoot, ValueObejct and Entity
    /// </summary>
    public class UserId : AggregateRootId<Guid>
    {

        public override Guid Value { get; protected set; }

        public UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUnque()
        {
            return new UserId(Guid.NewGuid());
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
