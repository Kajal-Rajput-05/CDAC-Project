using Application.Domain;
using Application.Domain.Context;
using Application.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class AdminRepository : IAdminRepository
    {
        public readonly ApplicationDBContext _dBContext;
        public AdminRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<int> CreateStadium(Stadium request)
        {
            try
            {
                var result = await _dBContext.Stadium.AddAsync(request);
                await _dBContext.SaveChangesAsync();
                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteStadium(int Id)
        {
            try
            {
                var result = await _dBContext.Stadium.FindAsync(Id);
                _dBContext.Remove(result);
                return "Stadium Delete Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<List<Stadium>> GetStadiumList()
        {
            try
            {
                return await _dBContext.Stadium.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<dynamic> GetTotalEventList()
        {
            try
            {
                dynamic result = await(from edata in _dBContext.Event
                                       join sdata in _dBContext.Stadium
                                       on edata.StadiumId equals sdata.Id
                                       join udata in _dBContext.User
                                       on edata.UserId equals udata.Id
                                       select new
                                       {
                                           Id = edata.Id,
                                           CreateTime = edata.CreateTime,
                                           UserId = edata.UserId,
                                           stadium = sdata,
                                           BookingDate = edata.BookingDate.ToString("dd-MM-yyyy"),
                                           Title = edata.Title,
                                           Description = edata.Description,
                                           PaymentType = edata.PaymentType,
                                           IsPaid = edata.IsPaid,
                                           Status = edata.Status,
                                           Capacity = edata.Capacity,
                                           ArtistType = udata.ArtistType,
                                       }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> UpdateStadium(Stadium request)
        {
            try
            {
                var result = await _dBContext.Stadium.FindAsync(request.Id);
                if (result is null)
                    throw new Exception("Stadium Detail Not Found");

                result.Location = request.Location;
                result.Description = request.Description;
                result.Name = request.Name;
                result.Price = request.Price;
                result.Capacity = request.Capacity;
                await _dBContext.SaveChangesAsync();
                return "Update Stadium Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
