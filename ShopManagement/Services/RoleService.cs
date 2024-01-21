using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role? GetById(int id)
        {
            Role? role = _roleRepository.GetById(id);
            return role;
        }

        public List<Role> GetAll()
        {
            List<Role> roles = _roleRepository.GetAll().ToList();
            return roles;
        }

        public Role Add(Role role)
        {
            Role? newRole = _roleRepository.AddAndSaveChanges(role);

            return newRole;
        }

        public void Update(Role role)
        {
            _roleRepository.UpdateAndSaveChanges(role);
        }

        public void Delete(int id)
        {
            Role? role = _roleRepository.GetById(id);

            _roleRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
