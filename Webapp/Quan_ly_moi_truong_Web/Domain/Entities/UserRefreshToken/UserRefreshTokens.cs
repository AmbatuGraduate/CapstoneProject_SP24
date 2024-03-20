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


    }
}
