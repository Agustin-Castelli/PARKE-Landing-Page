using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services.DTOs;
using PARKE_Landing_Page.Services.Interfaces;

namespace PARKE_Landing_Page.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService (IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin Create(AdminRequest admin)
        {
            var newObj = new Admin();

            newObj.Username = admin.Username;
            newObj.Password = admin.Password;

            return _adminRepository.Add(newObj);
        }

        public void Update(int id, AdminRequest admin)
        {
            var obj = _adminRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Admin), id);
            }

            if (obj.Username != string.Empty) obj.Username = admin.Username;
            if (obj.Password != string.Empty) obj.Password = admin.Password;

            _adminRepository.Update(obj);
        }

        public void Delete(int id)
        {
            var user = _adminRepository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException(nameof(Admin), id);
            }

            _adminRepository.Delete(user);
        }

        public Admin GetById(int id)
        {
            var obj = _adminRepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(Admin), id);
            }

            else
            {
                return obj;
            }
        }

        public List<Admin> GetAll()
        {
            return _adminRepository.GetAll();
        }
    }
}
