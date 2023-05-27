namespace Service.Interfaces
{
    public interface ISendMailService
    {
        Task SendVerificationEmail(string userEmail, string verificationToken);
    }
}
