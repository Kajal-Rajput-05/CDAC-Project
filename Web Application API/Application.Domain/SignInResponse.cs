using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class SignInResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string? Token { get; set; }
    }

    public class GetBookingListByUserResponse
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
        public Stadium stadium { get; set; }
        public string BookingDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public PaymentType PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public Status Status { get; set; }
        public int Capacity { get; set; }
        public ArtistType ArtistType { get; set; }
    }

    public class GetBookingListByArtist : GetBookingListByUserResponse
    {
        public string Username { get; set; }
        public int ArtistUserId { get; set; }
    }
}
