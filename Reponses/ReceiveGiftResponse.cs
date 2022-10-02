using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationTest.Entities;

namespace NotificationTest.Reponses
{
    public class ReceiveGiftResponse
    {
        public Item Item { get; set; } = default!;
        public int Quantity { get; set; }
    }
}