using Domain.Entities.Street;
using Domain.Entities.Ward;
using System.Runtime.Serialization;

namespace Domain.Entities.ResidentialGroup
{
    [DataContract]
    public class ResidentialGroups
    {
        [DataMember]
        public Guid ResidentialGroupId { get; set; }

        [DataMember]
        public string ResidentialGroupName { get; set; }

        [DataMember]
        public Guid WardId { get; set; }

        public virtual Wards? Ward { get; set; }
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