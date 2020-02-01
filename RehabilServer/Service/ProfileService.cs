using RehabilServer.Models;
using RehabilServer.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.Service
{
    public class ProfileService
    {
        private readonly IRepository<Profile> repository;
        private readonly IUnitOfWork unitOfWork;
        public ProfileService(IRepository<Profile> repository,IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public int Add(Profile entity)
        {
            if (repository.Add(entity) == 1)
            {
                unitOfWork.SaveChange();
                return 1;
            }
            return 0;
        }

        public int Delete(Profile entity)
        {
            if (repository.Delete(entity) == 1)
            {
                unitOfWork.SaveChange();
                return 1;
            }
            return 0;
        }

        public int Delete(Guid id)
        {
            if (repository.Delete(e => e.Id == id) == 1)
            {
                unitOfWork.SaveChange();
                return 1;
            }
            return 0;
        }

        public Profile FindById(Guid id)
        {
            return repository.FindById(id);
        }

        public IEnumerable<Profile> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Profile entity)
        {
            repository.Update(entity);
            unitOfWork.SaveChange();
        }
    }
}