using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchageRates.Repository
{
    public interface IExhangeRepository<Exchange>
    {
        IList<Exchange> GetAll();
        Exchange GetById(object id);
        void Insert(Exchange entity);
        void Update(Exchange entity);
        void Delete(object id);
    }
}
