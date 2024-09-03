using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Infrastructure
{
    public interface IAdminService
    {
        Task<int> CreateStadium(Stadium request);
        Task<List<Stadium>> GetStadiumList();
        Task<string> DeleteStadium(int Id);
        Task<string> UpdateStadium(Stadium request);
        Task<dynamic> GetTotalEventList();
    }
}
