using Domain.Entities.TreeType;
using Domain.Entities.User;
using System.Runtime.Serialization;

namespace Domain.Entities.Tree
{
    [DataContract]
    public class Trees
    {
        [DataMember]
        public Guid TreeId { get; set; }

        [DataMember]
        public string TreeCode { get; set; }

        [DataMember]
        public string TreeLocation { get; set; }

        [DataMember]
        public float BodyDiameter { get; set; }

        [DataMember]
        public float LeafLength { get; set; }

        [DataMember]
        public DateTime PlantTime { get; set; }

        [DataMember]
        public DateTime CutTime { get; set; }

        [DataMember]
        public int IntervalCutTime { get; set; }

        [DataMember]
        public Guid TreeTypeId { get; set; }

        public virtual TreeTypes? TreeType { get; set; }

        [DataMember]
        public string Note { get; set; } = string.Empty;

        [DataMember]
        public bool isCut { get; set; } = true;


        [DataMember]
        public string UserId { get; set; }

        public virtual Users? user { get; set; }
    }
}