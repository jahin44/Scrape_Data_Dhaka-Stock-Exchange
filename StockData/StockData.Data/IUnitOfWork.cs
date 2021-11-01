﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
