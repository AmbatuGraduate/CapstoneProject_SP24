using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Tree
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string District { get; set; } = string.Empty;
        [DataMember]
        public string Street { get; set; } = string.Empty;
        [DataMember]
        public string RootType { get; set; } = string.Empty;
        [DataMember]
        public string Type { get; set; } = string.Empty;
        [DataMember]
        public float BodyDiameter { get; set; }
        [DataMember]
        public float LeafLength { get; set; }
        [DataMember]
        public DateTime PlantTime { get; set; }
        [DataMember]
        public DateTime CutTime { get; set; }
        [DataMember]
        public string Note { get; set; } = string.Empty;
    }
}

