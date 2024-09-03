using Application.Domain;
using Application.Repository;
using Application.Repository.Infrastructure;
using Application.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> CreateBooking(Booking request)
        {
            return await _customerRepository.CreateBooking(request);
        }

        public async Task<string> DeleteBooking(int Id, Status Status)
        {
            return await _customerRepository.DeleteBooking(Id, Status);
        }

        public async Task<dynamic> GetBookingListByUser(int UserId)
        {
            return await _customerRepository.GetBookingListByUser(UserId);
        }

        public async Task<dynamic> GetEventList(int UserId)
        {
            return await _customerRepository.GetEventList(UserId);
        }

        public async Task<dynamic> GetEventList()
        {
            return await _customerRepository.GetEventList();
        }

        public async Task<string> Payment(Booking request)
        {
            return await _customerRepository.Payment(request);
        }
        public async Task<dynamic> GetFilterEventList(ArtistType artistType)
        {
            return await _customerRepository.GetFilterEventList(artistType);
        }

    }
}
