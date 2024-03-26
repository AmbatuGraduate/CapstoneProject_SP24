
using Domain.Entities.Tree;
using System.Runtime.Serialization;

namespace Domain.Entities.TreeType
{
    [DataContract]
    public class TreeTypes
    {
        [DataMember]
        public Guid TreeTypeId { get; set; }

        [DataMember]
        public string TreeTypeName { get; set; }
        public ICollection<Trees>? trees { get; set; }

    }
}