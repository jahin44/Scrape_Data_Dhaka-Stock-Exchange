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
    public class StockPriceService : IStockPriceService
    {
        private readonly IManagerUnitOfWork _managerUnitOfWork;
        private readonly IMapper _mapper;
        public StockPriceService(IManagerUnitOfWork managerUnitOfWork, IMapper mapper)
        {
            _managerUnitOfWork = managerUnitOfWork;
            _mapper = mapper;
        }

        public IList<StockPrice> GetAllStockPrices()
        {
            var stockPriceEntities = _managerUnitOfWork.StockPrices.GetAll();
            var stockPrices = new List<StockPrice>();
            foreach (var entity in stockPriceEntities)
            {
                var stockPrice = _mapper.Map<StockPrice>(entity);
                stockPrices.Add(stockPrice);
            }
            return stockPrices;
        }
        public void CreateStockPrice(StockPrice stockPrice)
        {

            _managerUnitOfWork.StockPrices.Add(
                _mapper.Map<Entities.StockPrice>(stockPrice)
            );
            _managerUnitOfWork.Save();
        }
        public StockPrice GetStockPrice(int id)
        {
            var stockPrice = _managerUnitOfWork.StockPrices.GetById(id);
            if (stockPrice == null) return null;
            return _mapper.Map<StockPrice>(stockPrice);
        }
    }
}
