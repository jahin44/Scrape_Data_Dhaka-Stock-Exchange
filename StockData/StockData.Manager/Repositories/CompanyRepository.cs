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
    public class CompanyRepository : Repository<Company, int>,
        ICompanyRepository
    {

        public CompanyRepository(IStockDataDbContext context)
            : base((DbContext)context)
        {

        }
    }
}
