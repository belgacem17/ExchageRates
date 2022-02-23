using ExchageRates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchageRates.Repository
{
    public class ExchangeRepository : IExhangeRepository<Exchange>
    {


        private readonly DataContext _dataContext;
        private DbSet<Exchange> table = null;
        public ExchangeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            table = _dataContext.Set<Exchange>();
        }


        public void Delete(object id)
        {
            Exchange exchange = table.Find(id);
            table.Remove(exchange);
            _dataContext.SaveChanges();
        }

        public IList<Exchange> GetAll()
        {
           return table.ToList();
        }

        public Exchange GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(Exchange entity)
        {
            table.Add(entity);
            _dataContext.SaveChanges();
        }

        public void Update(Exchange entity)
        {
            table.Attach(entity);
            _dataContext.SaveChanges();
        }
    }
}
