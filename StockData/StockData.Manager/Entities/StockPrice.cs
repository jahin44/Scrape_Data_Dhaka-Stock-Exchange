using StockData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Manager.Entities
{
    public class StockPrice : IEntity<int>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public decimal LastTradingPrice { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal YesterdayClosePrice { get; set; }
        public decimal Change { get; set; }
        public decimal Trade { get; set; }
        public decimal Value { get; set; }
        public decimal Volume { get; set; }
        public Company Company { get; set; }
    }
}
