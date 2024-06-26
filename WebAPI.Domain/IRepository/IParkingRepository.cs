﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public interface IParkingRepository : IBaseCompanyRepository<Parking>
    {
        Task<List<Parking>> GetParkingsByCompanyIdAsync(Guid companyId);
    }
}
