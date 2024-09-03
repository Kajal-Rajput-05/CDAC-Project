using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Infrastructure
{
    public interface ICustomerService
    {
        Task<dynamic> GetEventList(int UserId);
        Task<string> Payment(Booking request);
        Task<dynamic> GetEventList();
        Task<string> CreateBooking(Booking request);
        Task<dynamic> GetBookingListByUser(int UserId);
        Task<string> DeleteBooking(int Id, Status Status);
        Task<dynamic> GetFilterEventList(ArtistType artistType);

    }
}
