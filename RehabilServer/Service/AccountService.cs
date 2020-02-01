using RehabilServer.Models;
using RehabilServer.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehabilServer.Service
{

    public interface IAccountService 
    {
         int Add(Account entity);
         int Delete(Account entity);
         int Delete(Guid id);
         Account FindById(Guid id);
         IEnumerable<Account> GetAll();
        void Update(Account entity);
    }
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> repository;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IRepository<Account> repository,IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public  int Add(Account entity)
        {
            if (repository.Add(entity)==1)
            {
                unitOfWork.SaveChange();
                return 1;
            }
            return 0;
        }

        public int Delete(Account entity)
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
            if(repository.Delete(e => e.Id == id) == 1)
            {
                unitOfWork.SaveChange();
                return 1;
            }
            return 0;
        }

        public Account FindById(Guid id)
        {
            return repository.FindById(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return repository.GetAll();
        }

        public void Update(Account entity)
        {
            repository.Update(entity);
            unitOfWork.SaveChange();
        }
    }
}
