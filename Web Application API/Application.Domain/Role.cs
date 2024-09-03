using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public enum Role
    {
        ADMIN=1,
        CUSTOMER=2,
        ARTIST=3,
    }

    public enum Status
    {
        PENDING=1,
        BOOKED=2,
        CANCELLED=3,
    }

    public enum PaymentType
    {
        CASH=1,
        CARD=2,
        UPI=3,
        NONE=4,
    }
    
    public enum ArtistType
    {
        SINGER=1,
        DANCER=2,
        STANDUP_COMEDIAN=3,
        MUSICIAN=4
    }
}
