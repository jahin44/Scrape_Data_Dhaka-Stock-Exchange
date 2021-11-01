using StockData.Data;
using StockData.Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.UnitOfWorks
{
    public interface IManagerUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companys  { get;  }
        IStockPriceRepository StockPrices { get; }
    }
}