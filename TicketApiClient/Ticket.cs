using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApiClient
{
    public class Ticket
    {
        public string Uid { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public int Price { get; set; }
        public string ContentType { get; set; }

        public byte[] PictureData { get; set; }

        public IdentityUser Seller { get; set; }
    }
}
