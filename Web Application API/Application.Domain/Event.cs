using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int StadiumId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime BookingDate { get; set; }
        public int Capacity { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.NONE;
        public string PaymentDetail { get; set; }
        public bool IsPaid { get; set; } = false;
        public Status Status { get; set; } = Status.PENDING;

    }
}
