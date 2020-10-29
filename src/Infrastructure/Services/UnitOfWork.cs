using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dataContext;
        private bool disposed;

         

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _dataContext = serviceProvider
                       .GetService<ApplicationDbContext>(); 
        }


        public IGenericRepository<T> GetRepository<T>() where T: class
        {
            return new GenericRepository<T>(_dataContext);
        }

        public int Save()
        {
            //if (_dataContext.GetValidationErrors().Any())
            //{
            //    throw (new Exception(_dataContext.GetValidationErrors().ToList()[0].ValidationErrors.ToList()[0].ErrorMessage)) ;
            //}
            return _dataContext.SaveChanges();
        }
        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            //if (_dataContext.GetValidationErrors().Any())
            //{
            //    throw (new Exception(_dataContext.GetValidationErrors().ToList()[0].ValidationErrors.ToList()[0].ErrorMessage)) ;
            //}
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                    disposed = true;
                }
            }
            disposed = false;
        }

    }
}
