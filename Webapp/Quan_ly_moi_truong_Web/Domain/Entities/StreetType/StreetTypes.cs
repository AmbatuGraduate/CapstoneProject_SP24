using Domain.Entities.Street;
using System.Runtime.Serialization;

namespace Domain.Entities.StreetType
{
    [DataContract]
    public class StreetTypes
    {
        [DataMember]
        public Guid StreetTypeId { get; set; }

        [DataMember]
        public string StreetTypeName { get; set; }

        public ICollection<Streets>? Streets { get; set; }

        [DataMember]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [DataMember]
        public string CreateBy { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        [DataMember]
        public string UpdateBy { get; set; }
    }
}