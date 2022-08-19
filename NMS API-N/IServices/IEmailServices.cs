namespace NMS_API_N.IServices
{
    public interface IEmailServices
    {
        public bool SendMail(string toMail, string subject, string body);
    }
}
