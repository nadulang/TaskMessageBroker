using System;
using System.Collections.Generic;

namespace NotificationService.Application.UseCases.Notifications.Request
{
    public class NotifQueryDto 
    {
        public Notifications2_ notification { get; set; }
        public List<Logs_> logs { get; set; }
    }

    public class Notifications2_
    {
        public int id { get; set; }
        public string title { get; set; }
        public string message { get; set; }
    }

    public class Logs_
    {
        public int notification_id { get; set; }
        public string type { get; set; }
        public int from { get; set; }
        public int target { get; set; }
        public long read_at { get; set; }
    }
}
