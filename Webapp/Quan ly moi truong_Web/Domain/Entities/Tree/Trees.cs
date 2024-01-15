using Domain.Entities.Cultivar;
using Domain.Entities.Street;
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
        public Guid StreetId { get; set; }
        public virtual Streets? Streets { get; set; }
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
        public Guid CultivarId { get; set; }
        public virtual Cultivars? Cultivar { get; set; }
        [DataMember]
        public string Note { get; set; } = string.Empty;

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
