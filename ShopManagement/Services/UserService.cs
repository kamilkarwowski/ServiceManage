using ServiceManagement.DAL;
using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? GetById(int id)
        {
            User? user = _userRepository.GetById(id);
            return user;
        }

        public List<User> GetAll()
        {
            List<User> users = _userRepository.GetAll().ToList();
            return users;
        }

        public User Add(User user) 
        { 
            User? newUser = _userRepository.AddAndSaveChanges(user);

            return newUser;
        }

        public void Update(User user)
        {
            _userRepository.UpdateAndSaveChanges(user);
        }

        public void Delete(int id)
        {
            User? user = _userRepository.GetById(id);

            _userRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
