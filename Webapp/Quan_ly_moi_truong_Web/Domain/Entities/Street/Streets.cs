using Domain.Entities.ResidentialGroup;
using Domain.Entities.StreetType;
using Domain.Entities.Tree;

using System.Runtime.Serialization;

namespace Domain.Entities.Street
{
    [DataContract]
    public class Streets
    {
        [DataMember]
        public Guid StreetId { get; set; }

        [DataMember]
        public string StreetName { get; set; }

        [DataMember]
        public float StreetLength { get; set; }

        [DataMember]
        public int NumberOfHouses { get; set; }

        [DataMember]
        public Guid StreetTypeId { get; set; }

        public virtual StreetTypes? StreetType { get; set; }

        [DataMember]
        public Guid ResidentialGroupId { get; set; }

        public virtual ResidentialGroups? ResidentialGroup { get; set; }

        public ICollection<Trees>? Trees { get; set; }

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