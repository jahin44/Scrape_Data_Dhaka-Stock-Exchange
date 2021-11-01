using AutoMapper;
using StockData.Manager.Entities;
using StockData.Manager.Exceptions;
using StockData.Manager.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IManagerUnitOfWork _managerUnitOfWork;
        private readonly IMapper _mapper;
        public CompanyService(IManagerUnitOfWork managerUnitOfWork, IMapper mapper)
        {
            _managerUnitOfWork = managerUnitOfWork;
            _mapper = mapper;
        }

        public IList<Company> GetAllCompanys()
        {
            var companyEntities = _managerUnitOfWork.Companys.GetAll();
            var companys = new List<Company>();
            foreach(var entity in companyEntities)
            {
                var company = _mapper.Map<Company>(entity);
                companys.Add(company);
            }
            return companys;
        }
        public void CreateCompany(Company company)
        {
            if (IsTitleAlreadyUsed(company.TradeCode) == false)
            {
                _managerUnitOfWork.Companys.Add(
                    _mapper.Map<Entities.Company>(company)
                );
                _managerUnitOfWork.Save();
            }
        }
         
        public Company GetCompany(int id)
        {
            var company = _managerUnitOfWork.Companys.GetById(id);
            if (company == null) return null;
            return _mapper.Map<Company>(company);
        }

        private bool IsTitleAlreadyUsed(string tradeCode) =>
            _managerUnitOfWork.Companys.GetCount(x => x.TradeCode == tradeCode) > 0;


    }
}

