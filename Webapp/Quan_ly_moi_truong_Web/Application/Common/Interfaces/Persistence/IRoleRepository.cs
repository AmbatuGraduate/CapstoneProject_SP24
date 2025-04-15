using Domain.Entities.Role;

namespace Application.Common.Interfaces.Persistence
{
    public interface IRoleRepository
    {
        List<Roles> GetRoles();

        Roles GetRole(Guid id);
    }
}