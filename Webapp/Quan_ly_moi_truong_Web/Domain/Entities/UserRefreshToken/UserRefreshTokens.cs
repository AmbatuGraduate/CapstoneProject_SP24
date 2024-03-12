using Domain.Entities.User;
using System.Runtime.Serialization;

namespace Domain.Entities.UserRefreshToken
{
    [DataContract]
    public class UserRefreshTokens
    {
        [DataMember]
        public Guid UserRefreshTokenId { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string RefreshToken { get; set; }

        [DataMember]
        public long Expire { get; set; }

        public virtual Users? User { get; set; }

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