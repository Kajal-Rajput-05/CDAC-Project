using Application.Domain;
using Application.Domain.Context;
using Application.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        public readonly ApplicationDBContext _dBContext;
        public ArtistRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<dynamic> GetEventList(int UserId)
        {
            try
            {

                dynamic result = await (from edata in _dBContext.Event
                                    join sdata in _dBContext.Stadium
                                    on edata.StadiumId equals sdata.Id
                                    where edata.UserId == UserId
                                    select new
                                    {
                                        Id = edata.Id,
                                        CreateTime = edata.CreateTime,
                                        UserId = edata.UserId,
                                        stadium = sdata,
                                        BookingDate = edata.BookingDate.ToString("yyyy-MM-dd"),
                                        Title = edata.Title,
                                        Description = edata.Description,
                                        PaymentType = edata.PaymentType,
                                        IsPaid = edata.IsPaid,
                                        Status = edata.Status,
                                        Capacity = edata.Capacity
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<int> CreateEvent(Event request)
        {
            try
            {
                var result = await _dBContext.Event.AddAsync(request);
                await _dBContext.SaveChangesAsync();
                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteEvent(int Id, Status Status)
        {
            try
            {
                var result = await _dBContext.Event.FindAsync(Id);
                result.Status = Status;
                await _dBContext.SaveChangesAsync();
                return "Cancel Event Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateEvent(Event request)
        {
            try
            {
                var result = await _dBContext.Event.FindAsync(request.Id);
                if (result is null)
                    throw new Exception("Event Detail Not Found");

                result.BookingDate = request.BookingDate;
                result.Title = request.Title;
                result.Description = request.Description;
                await _dBContext.SaveChangesAsync();
                return "Update Event Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Payment(Event request)
        {
            try
            {
                var result = await _dBContext.Event.FindAsync(request.Id);
                if (result is null)
                    throw new Exception("Event Detail Not Found");

                result.PaymentType = request.PaymentType;
                result.PaymentDetail = request.PaymentDetail;
                result.IsPaid = true;
                await _dBContext.SaveChangesAsync();
                return "Update Event Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<dynamic> GetBookingListByArtist(int UserId)
        {
            try
            {
                var result = await _dBContext.Booking.ToListAsync();
                List<GetBookingListByArtist> data = new List<GetBookingListByArtist>();
                foreach (var item in result)
                {
                    GetBookingListByArtist r = new GetBookingListByArtist();
                    r.Id = item.Id;
                    r.CreateTime = item.CreatedDate;
                    r.UserId = item.UserId;

                    var e = await _dBContext.Event.FindAsync(item.EventId);
                    r.ArtistUserId = e.UserId;
                    r.stadium = _dBContext.Stadium.FindAsync(e.StadiumId).Result;
                    r.BookingDate = e.BookingDate.ToString("dd-MM-yyyy");
                    r.Title = e.Title;
                    r.Price = item.TotalPrice;
                    r.Description = e.Description;
                    r.PaymentType = item.PaymentType;
                    r.IsPaid = item.IsPaid;
                    r.Status = item.Status;
                    r.Capacity = item.Quentity;
                    
                    r.ArtistType = _dBContext.User.FindAsync(e.UserId).Result.ArtistType;
                    r.Username = _dBContext.User.FindAsync(item.UserId).Result.UserName;
                    data.Add(r);
                }

                var final = data.Where(x2 => x2.ArtistUserId == UserId);

                return final;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
