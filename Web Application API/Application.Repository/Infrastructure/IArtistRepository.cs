using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Infrastructure
{
    public interface IArtistRepository
    {
        Task<dynamic> GetEventList(int UserId);
        Task<int> CreateEvent(Event request);
        Task<string> DeleteEvent(int Id, Status Status);
        Task<string> UpdateEvent(Event request);
        Task<string> Payment(Event request);
        Task<dynamic> GetBookingListByArtist(int UserId);

    }
}
