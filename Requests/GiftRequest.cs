using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Requests
{
    public class GiftRequest
    {
        public Guid ReceivePlayerId { get; set; }
        public string ItemCode { get; set; } = default!;
    }
}