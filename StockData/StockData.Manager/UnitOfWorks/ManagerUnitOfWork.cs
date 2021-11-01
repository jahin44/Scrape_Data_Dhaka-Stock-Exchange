using StockData.Data;
using StockData.Manager.Contexts;
using StockData.Manager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.UnitOfWorks
{
    public class ManagerUnitOfWork : UnitOfWork, IManagerUnitOfWork
    {
        public ICompanyRepository Companys { get; private set; }
        public IStockPriceRepository StockPrices { get;private set; }
        public ManagerUnitOfWork(IStockDataDbContext context,
            IStockPriceRepository stockPrices,
            ICompanyRepository companys
            ) : base((DbContext)context)
        {
            StockPrices = stockPrices;
            Companys = companys;
        }


    }
}
