using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationTest.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; } = default!;
        public string? Subject { get; set; }
        public string? To { get; set; }
        public string? Content { get; set; }
        public string? Attach { get; set; }
    }
}