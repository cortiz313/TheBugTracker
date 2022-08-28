namespace TheBugTracker.Models
{
    public class MailSettings
    {   // this is not a data model, just a simple class, dont need an id
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
