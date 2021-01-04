﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.EFCore;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyMDBController<User, EfCoreUserRepository>
    {
        public UserController(EfCoreUserRepository repository) : base(repository)
        {

        }
    }
}
