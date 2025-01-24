using PARKE_Landing_Page.Data.Interfaces;
using PARKE_Landing_Page.Data.Repositories;
using PARKE_Landing_Page.Models.Entities;
using PARKE_Landing_Page.Models.Exceptions;
using PARKE_Landing_Page.Services.DTOs;

namespace PARKE_Landing_Page.Services
{
    public class NewService
    {
        private readonly INewRepository _newrepository;

        public NewService(INewRepository newrepository)
        {
            _newrepository = newrepository;
        }

        public New Create(NewRequest ndto)
        {
            var newObj = new New();

            newObj.Title = ndto.Title;
            newObj.Description = ndto.Description;
            newObj.Date = ndto.Date;
            newObj.Image = ndto.Image;

            return _newrepository.Add(newObj);
        }

        public New GetById(int id)
        {
            var obj = _newrepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(New), id);
            }

            else
            {
                return obj;
            }
        }
        public List<New> GetAll()
        {
            return _newrepository.GetAll();
        }

        public void Update(int id, NewRequest nnew)
        {
            var obj = _newrepository.GetById(id);

            if (obj == null)
            {
                throw new NotFoundException(nameof(New), id);
            }

            if (obj.Title != string.Empty) obj.Title = nnew.Title;
            if (obj.Description != string.Empty) obj.Description = nnew.Description;
            if (obj.Date != string.Empty) obj.Date = nnew.Date;
            if (obj.Image != string.Empty) obj.Image = nnew.Image;

            _newrepository.Update(obj);
        }

        public void Delete(int id)
        {
            var nnew = _newrepository.GetById(id);

            if (nnew == null)
            {
                throw new NotFoundException(nameof(New), id);
            }

            _newrepository.Delete(id);
        }
    }
    }
