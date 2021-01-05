﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.EFCore;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : MyMDBController<Territory, TerritoryService>
    {
        public TerritoryController(TerritoryService service) : base(service)
        {

        }
    }
}
