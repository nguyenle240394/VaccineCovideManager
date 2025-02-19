﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace VaccineCovidManager.VaccineCovids
{
    public class VaccineAppService : ApplicationService, IVaccineAppService
    {
        private readonly IVaccineCovidRepository _vaccineRepository;

        public VaccineAppService(IVaccineCovidRepository vaccineRepository)
        {
            _vaccineRepository = vaccineRepository;
        }

        public async Task<PagedResultDto<VaccineCovidDto>> GetListAsync(GetVaccineInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(VaccineCovid.CreationTime);
            }
            var vaccine = await _vaccineRepository.GetListAsync(
                    input.SkipCount,
                    input.MaxResultCount,
                    input.Sorting,
                    input.Filter
                );
            var vaccinedto = ObjectMapper.Map<List<VaccineCovid>, List<VaccineCovidDto>>(vaccine);
            var count = await _vaccineRepository.GetCountAsync();
            var stt = 1;
            foreach(var item in vaccinedto)
            {
                item.Stt = stt;
                stt++;
            }
            return new PagedResultDto<VaccineCovidDto>(
                    count,
                    vaccinedto);
        }

        public async Task<VaccineCovidDto> GetVaccineCovidAsync(Guid id)
        {
            var vaccinecovid = await _vaccineRepository.FindAsync(id);
            return ObjectMapper.Map<VaccineCovid, VaccineCovidDto>(vaccinecovid);
        }

        public async Task<VaccineCovidDto> CreateAsync(CreateUpdateVaccineDto input)
        {
            var vaccine = ObjectMapper.Map<CreateUpdateVaccineDto, VaccineCovid>(input);
            await _vaccineRepository.InsertAsync(vaccine);
            return ObjectMapper.Map<VaccineCovid, VaccineCovidDto>(vaccine);
        }

        public async Task<VaccineCovidDto> UpdateAsync(Guid id, CreateUpdateVaccineDto input)
        {
            var vaccine = await _vaccineRepository.FindAsync(id);
            vaccine.TenVaccine = input.TenVaccine;
            vaccine.SoLuongTonKho = input.SoLuongTonKho;
            await _vaccineRepository.UpdateAsync(vaccine);
            return ObjectMapper.Map<VaccineCovid, VaccineCovidDto>(vaccine);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var vaccine = await _vaccineRepository.FindAsync(id);
            if (vaccine != null)
            {
                await _vaccineRepository.DeleteAsync(vaccine);
                return true;
            }
            return false;
        }

        public async Task<VaccineCovidDto> FindVaccineById(Guid id)
        {
            var vaccine = await _vaccineRepository.FindAsync(id);
            return ObjectMapper.Map<VaccineCovid, VaccineCovidDto>(vaccine);
        }
    }
}
