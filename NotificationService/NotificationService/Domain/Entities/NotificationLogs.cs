namespace NotificationService.Domain.Entities
{
    public class NotificationLogs_
    {
        public int id { get; set; }
        public int notification_id { get; set; }
        public string type { get; set; }
        public int from { get; set; }
        public int target { get; set; }
        public string email_destination { get; set; }
        public long read_at { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }

        public Notifications_ notification { get; set; }
    }

   
}
