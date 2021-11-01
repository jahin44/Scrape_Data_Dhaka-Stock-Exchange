using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockData.Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using StockData.Manager.Entities;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;       
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;
        string html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";

        HtmlWeb web = new HtmlWeb();
        

        public Worker(ILogger<Worker> logger, ICompanyService companyService,
            IStockPriceService stockPriceService)
        {
            _logger = logger;
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                //if (IsClosed() is false)
                //{
                    DhakaStock();
               // }
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private  void DhakaStock()
        {

            var htmlDoc = web.Load(html);
            var nodes = htmlDoc.DocumentNode.CssSelect(".shares-table");
            var rows = nodes.FirstOrDefault()?.ChildNodes.Where(c => c.Name.Equals("tr"));
            foreach (var row in rows)
            {
                var data = row.ChildNodes.Where(n => n.Name.Equals("td")).ToArray();
                var company = new Company { TradeCode = Parse(data[1].InnerHtml) };
                _companyService.CreateCompany(company);

                var stockPrice = new StockPrice
                {
                    CompanyId = company.Id,
                    LastTradingPrice = ToDecimal(Parse(data[2].InnerHtml)),
                    High = ToDecimal(Parse(data[3].InnerHtml)),
                    Low = ToDecimal(Parse(data[4].InnerHtml)),
                    ClosePrice = ToDecimal(Parse(data[5].InnerHtml)),
                    YesterdayClosePrice = ToDecimal(Parse(data[6].InnerHtml)),
                    Change = ToDecimal(Parse(data[7].InnerHtml).Equals("--") ? "0" : Parse(data[7].InnerHtml)),
                    Trade = ToDecimal(Parse(data[8].InnerHtml)),
                    Value = ToDecimal(Parse(data[9].InnerHtml)),
                    Volume = ToDecimal(Parse(data[10].InnerHtml)),
                    Company = company
                };

                
                _stockPriceService.CreateStockPrice(stockPrice);               
            }            
        }
        private bool IsClosed()
        {
            var htmlDoc = web.Load(html);
            var nodes = htmlDoc.DocumentNode.CssSelect(".HeaderTop");
            var data = nodes.Select(node => node.ChildNodes)
                .FirstOrDefault().LastOrDefault(s => s.Name.Equals("span"))
                ?.ChildNodes.LastOrDefault().ChildNodes.FirstOrDefault().InnerHtml;
            return data != null && (data.Equals("Closed") ? true : false);
        }
        private decimal ToDecimal(string number)
        {
            number = Parse(number);
            if (number.Equals("--")) number = "0";
            return Convert.ToDecimal(number);
        }
        private string Parse(string data)
        {
            data = data.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            return Regex.Replace(data, "<.*?>", String.Empty);
        }


    }
}
