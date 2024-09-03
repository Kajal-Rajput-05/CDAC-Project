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
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<dynamic> GetEventList(int UserId)
        {
            return await _artistRepository.GetEventList(UserId);
        }
        public async Task<int> CreateEvent(Event request)
        {
            return await _artistRepository.CreateEvent(request);
        }

        public async Task<string> DeleteEvent(int Id, Status Status)
        {
            return await _artistRepository.DeleteEvent(Id, Status);
        }

        public async Task<string> UpdateEvent(Event request)
        {
            return await _artistRepository.UpdateEvent(request);
        }

        public async Task<string> Payment(Event request)
        {
            return await _artistRepository.Payment(request);
        }

        public async Task<dynamic> GetBookingListByArtist(int UserId)
        {
            return await _artistRepository.GetBookingListByArtist(UserId);
        }
    }
}
