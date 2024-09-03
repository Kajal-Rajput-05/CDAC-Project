using Application.Domain;
using Application.Repository.Infrastructure;
using Application.Service.Infrastructure;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
        }

        public async Task<int> CreateStadium(Stadium request)
        {
            return await _adminRepository.CreateStadium(request);
        }

        public async Task<string> DeleteStadium(int Id)
        {
            return await _adminRepository.DeleteStadium(Id);
        }

       

        public async Task<List<Stadium>> GetStadiumList()
        {
            return await _adminRepository.GetStadiumList();
        }

        public async Task<dynamic> GetTotalEventList()
        {
           return await _adminRepository.GetTotalEventList();
        }

        public async Task<string> UpdateStadium(Stadium request)
        {
            return await _adminRepository.UpdateStadium(request);
        }
    }
}
