using Application.Domain;
using Application.Domain.Context;
using Application.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Core;

namespace Application.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly ApplicationDBContext _dBContext;
        private readonly IConfiguration _configuration;
        public CustomerRepository(ApplicationDBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public async Task<string> Payment(Booking request)
        {
            try
            {
                var result = await _dBContext.Booking.FindAsync(request.Id);
                if (result is null)
                    throw new Exception("Booking Detail Not Found");

                result.PaymentType = request.PaymentType;
                result.Status = Status.BOOKED;
                result.IsPaid = true;
                await _dBContext.SaveChangesAsync();

                var user = _dBContext.User.FindAsync(result.UserId).Result;

                var item = _dBContext.Booking.FindAsync(request.Id).Result;

                GetBookingListByUserResponse r = new GetBookingListByUserResponse();
                r.Id = item.Id;
                r.CreateTime = item.CreatedDate;
                r.UserId = item.UserId;

                var e = await _dBContext.Event.FindAsync(item.EventId);
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

                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration["fromMail"] ?? string.Empty);
                message.Subject = "Booking Confirmation Mail";
                message.To.Add(new MailAddress(user.Email.ToLower()));
                message.Body = CreateTemplate(user.Email, r);
                message.IsBodyHtml = true;
                var smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_configuration["fromMail"], _configuration["fromPassword"]),
                    EnableSsl = true,
                };
                smtpclient.Send(message);

                return "Event Payment Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                                            Id = edata.UserId,
                                            CreateTime = edata.CreateTime,
                                            UserId = edata.UserId,
                                            stadium = sdata,
                                            BookingDate = edata.BookingDate,
                                            Title = edata.Title,
                                            Description = edata.Description,
                                            PaymentType = edata.PaymentType,
                                            IsPaid = edata.IsPaid,
                                            Status = edata.Status,
                                        }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string CreateTemplate(string? email, GetBookingListByUserResponse request)
        {
            return @"
                        <html>
                          <head>
                            <style>
                              .container {
                                height: 100%;
                                width: 100%;
                                display: flex;
                                justify-content: center;
                                align-items: center;
                              }

                              .subContainer {
                                height: 70%;
                                width: 45%;
                              }

                              .google {
                                font-size: 26px;
                                height: 15%;
                                width: 100%;
                                display: flex;
                                align-items: center;
                                font-family: Helvetica;
                                font-weight: 400;
                              }

                              .body {
                                height: 85%;
                                width: 605px;
                                font-family: Helvetica;
                                font-size: 13px;
                                box-shadow: none;
                                border-radius: 0 0 3px 3px;
                              }

                              .header {
                                background-color: #4184f3;
                                height: 30%;
                                width: 99.5%;
                                margin-left: 0.25%;
                                border-radius: 3px 3px 0 0;
                              }

                              .h2 {
                                height: 100%;
                                font-size: 24px;
                                color: #ffffff;
                                font-weight: 400;
                                font-family: Helvetica;
                                margin: 0 0 0 40px;
                                display: flex;
                                align-items: center;
                              }
                              .subBody {
                                background-color: #fafafa;
                                height: 289px;
                                min-width: 332px;
                                max-width: 753px;
                                border: 1px solid #f0f0f0;
                                border-bottom: 1px solid #c0c0c0;
                              }
                              .innersubBody {
                                width: 86%;
                                height: 100%;
                                margin: 6% 6%;
                              }
                              /*.btn {
                                background-color: #4184f3;
                                color: white;
                                border-radius: 5px;
                                width: 200px;
                                height: 30px;
                                margin: 0 auto;
                                padding: 13px 0 0 15px;
                                cursor: pointer;
                              }*/
                            </style>
                          </head>
                          <body>
                            <div class='container'>
                              <div class='subContainer'>
                                <div class='google'>
                                </div>
                                <div class='body'>
                                  <div class='header' style='height: 120px;'>
                                    <div class='h2' style='height: 120px; padding: 40px 0;'>
                                      Concert Hub
                                    </div>
                                  </div>
                                  <div class='subBody'>
                                    <div class='innersubBody'>
                                      <div>
                                        Thank you for your Booking!
                                      </div>
                                      <br/>
                                      <div>Event Name " + request.Title + @"</div>
                                      <br/>
                                      <div>Event Date " + request.BookingDate + @"</div>
                                      <br/>
                                      <div>Ticket Price " + request.Price + @"</div>
                                      <br/>
                                      <div>Event Location " + request.stadium.Location + @"</div>
                                      <br/>
                                      <div>Don’t know why you received this?</div>
                                      <br />
                                      <div>
                                        Someone who couldn’t remember their Concert Account details
                                        probably gave your email address by mistake. You can safely
                                        ignore this email.
                                      </div>
                                      <br />
                                      <div>
                                        To protect your account, don’t forward this email or give this
                                        code to anyone.
                                      </div>
                                      <br />
                                      <div>Concert Hub Accounts team</div>
                                      <br />
                                    </div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </body>
                        </html>
                    ";
        }

        public async Task<dynamic> GetEventList()
        {
            try
            {
                dynamic result = await (from edata in _dBContext.Event
                                        join sdata in _dBContext.Stadium
                                        on edata.StadiumId equals sdata.Id
                                        join udata in _dBContext.User
                                        on edata.UserId equals udata.Id
                                        where edata.Status == Status.BOOKED && edata.IsPaid
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

        public async Task<string> CreateBooking(Booking request)
        {
            try
            {
                var result = await _dBContext.Booking.AddAsync(request);

                var eventResult = await _dBContext.Event.FindAsync(request.EventId);
                eventResult.Capacity = eventResult.Capacity - request.Quentity;
                _dBContext.Event.Update(eventResult);

                await _dBContext.SaveChangesAsync();
                return result.Entity.Id.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<dynamic> GetBookingListByUser(int UserId)
        {
            try
            {
                var result = await _dBContext.Booking.Where(x => x.UserId == UserId).ToListAsync();
                List < GetBookingListByUserResponse > data = new List < GetBookingListByUserResponse >();
                foreach (var item in result)
                {
                    GetBookingListByUserResponse r = new GetBookingListByUserResponse();
                    r.Id = item.Id;
                    r.CreateTime = item.CreatedDate;
                    r.UserId = item.UserId;
                    
                    var e = await _dBContext.Event.FindAsync(item.EventId);
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

                    data.Add(r);
                }
                

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteBooking(int Id, Status Status)
        {
            try
            {
                var result = await _dBContext.Booking.FindAsync(Id);
                if (result is null)
                    throw new Exception("Booking Detail Not Found");

                result.Status = Status;
                await _dBContext.SaveChangesAsync();
                return "Update Booking Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<dynamic> GetFilterEventList(ArtistType artistType)
        {
            try
            {
                dynamic result = await (from edata in _dBContext.Event
                                        join sdata in _dBContext.Stadium
                                        on edata.StadiumId equals sdata.Id
                                        join udata in _dBContext.User
                                        on edata.UserId equals udata.Id
                                        where udata.ArtistType == artistType
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

    }
}
