using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(UserRequest user)
        {
            var newObj = new User();

            newObj.Username = user.Username;
            newObj.Password = user.Password;

            return _userRepository.Add(newObj);
        }

        public void Update(int id, UserRequest user)
        {
            var obj = _userRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            if (obj.Username != string.Empty) obj.Username = user.Username;
            if (obj.Password != string.Empty) obj.Password = user.Password;

            _userRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            _userRepository.Delete(user);
        }

        public User GetById(int id)
        {
            var obj = _userRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(User), id);
            }

            else
            {
                return obj;
            }
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
