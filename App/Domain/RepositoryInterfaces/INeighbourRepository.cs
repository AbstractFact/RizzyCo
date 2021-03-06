﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface INeighbourRepository : IRepository<Neighbour>
    {
        Task<List<Neighbour>> GetTerritoryNeighbours(Territory terr);
    }
}
