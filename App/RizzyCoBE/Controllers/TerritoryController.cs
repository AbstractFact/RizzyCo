﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.Data.EFCore;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : MyMDBController<Territory, EfCoreTerritoryRepository>
    {
        public TerritoryController(EfCoreTerritoryRepository repository) : base(repository)
        {

        }
    }
}
