using StockData.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Services
{
    public interface IStockPriceService
    {
        IList<StockPrice> GetAllStockPrices();
        void CreateStockPrice(StockPrice stockPrice);

        StockPrice GetStockPrice(int id);
    }
}
