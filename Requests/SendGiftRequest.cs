using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Requests
{
    public class SendGiftRequest : GiftRequest
    {
        public Guid SendPlayerId { get; set; }
        public int Quantity { get; set; }
    }
}