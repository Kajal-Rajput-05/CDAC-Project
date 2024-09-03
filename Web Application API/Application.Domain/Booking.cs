using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class Booking
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Quentity {  get; set; }
        public int TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.NONE;
        public bool IsPaid { get; set; }
        public Status Status { get; set; } = Status.PENDING;

    }
}
