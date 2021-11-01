using Autofac;
using StockData.HtmlData.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.HtmlData
{
    public class HtmlDataModule : Module
    {

        private string _url;

        public HtmlDataModule(string url)
        {
            _url = url;
        }

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<HtmlPack>().As<IHtmlPack>()
                .WithParameter("url", _url)
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}