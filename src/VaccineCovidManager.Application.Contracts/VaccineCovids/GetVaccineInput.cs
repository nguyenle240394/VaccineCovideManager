﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManager.VaccineCovids
{
    public class GetVaccineInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
