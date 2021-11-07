using DeliverIT.Services.Contracts;

namespace DeliverIT.Services.Helpers
{
    public class MailSettings : IMailSettings
    {
        public string Mail => "project.deliverit@gmail.com";
        public string DisplayName => "DeliverIT";
        public string Password => "deliver247deliver";
        public string Host => "smtp.gmail.com";
        public int Port => 587;
    }
}
