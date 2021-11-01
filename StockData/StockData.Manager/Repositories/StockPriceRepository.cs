using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Manager.Contexts;
using StockData.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Repositories
{
    public class StockPriceRepository : Repository<StockPrice,int>,
        IStockPriceRepository
    {

        public StockPriceRepository(StockDataDbContext context)
              : base((DbContext)context)
        {
        }
    }
}
