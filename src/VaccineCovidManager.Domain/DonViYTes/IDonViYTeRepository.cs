﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace VaccineCovidManager.DonViYTes
{
    public interface IDonViYTeRepository : IRepository<DonViYTe, Guid>
    {
    }
}
