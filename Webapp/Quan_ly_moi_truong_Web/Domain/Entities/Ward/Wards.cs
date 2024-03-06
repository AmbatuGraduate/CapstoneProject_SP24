using Domain.Entities.District;
using Domain.Entities.ResidentialGroup;
using System.Runtime.Serialization;

namespace Domain.Entities.Ward
{
    [DataContract]
    public class Wards
    {
        [DataMember]
        public Guid WardId { get; set; }

        [DataMember]
        public string WardName { get; set; }

        [DataMember]
        public Guid DistrictId { get; set; }

        public virtual Districts? Districts { get; set; }
        public ICollection<ResidentialGroups>? ResidentialGroups { get; set; }
    }
}