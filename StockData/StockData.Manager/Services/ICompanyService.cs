using StockData.Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Services
{
    public interface ICompanyService
    {
        IList<Company> GetAllCompanys();
        void CreateCompany(Company company);
        Company GetCompany(int id);
       



    }
}
