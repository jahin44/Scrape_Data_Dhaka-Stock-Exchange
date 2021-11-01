using StockData.Manager.Contexts; 
using StockData.Data;
using StockData.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Repositories
{
    public interface IStockPriceRepository : IRepository<StockPrice,int>
    {

    }
}
