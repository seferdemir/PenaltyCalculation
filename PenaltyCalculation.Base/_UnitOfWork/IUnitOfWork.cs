using System.Data.Entity;

namespace PenaltyCalculation.Base
{
    public interface IUnitOfWork
    {
        DbContext context { get; }

        void Save();
    }

}
