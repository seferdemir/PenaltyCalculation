using System;
using System.Data.Entity;
using System.Diagnostics;

namespace PenaltyCalculation.Base
{
    public partial class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool disposed = false;

        public readonly ApplicationDbContext context = null;

        public UnitOfWork()
        {
            context = new ApplicationDbContext();
        }

        #region DbContext

        DbContext IUnitOfWork.context => throw new NotImplementedException();

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                //todo:log
                throw e;
            }
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

}
