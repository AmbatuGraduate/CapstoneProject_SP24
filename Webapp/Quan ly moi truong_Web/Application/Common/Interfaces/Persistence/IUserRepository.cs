using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        List<Users> GetAll();
        Users GetById(Guid id);
        Users? GetUserByPhone(string phoneNumber);
        void Add(Users user);

        void Update(Users user);
    }
}
