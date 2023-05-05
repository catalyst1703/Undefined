namespace Blogs.Services
{
    public class EmailSettings
    {
        public string SenderEmail { get; set; }

        public string SenderPassword { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

    }
}
