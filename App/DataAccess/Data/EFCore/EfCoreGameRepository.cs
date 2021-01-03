﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Models;
using DataAccess.Data.EFCore;

namespace DataAccess.Data.EFCore
{
    public class EfCoreGameRepository : EfCoreRepository<Game, RizzyCoContext>
    {
        public EfCoreGameRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}