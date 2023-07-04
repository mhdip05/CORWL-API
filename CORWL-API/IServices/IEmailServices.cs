namespace CORWL_API.IServices
{
    public interface IEmailServices
    {
        public bool SendMail(string toMail, string subject, string body);
    }
}
