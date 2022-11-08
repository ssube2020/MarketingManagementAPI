﻿using Core.Dtos;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IProductService
    {
        ApplicationResult<int?> Register(ProductAddDto addDto);
    }
}
