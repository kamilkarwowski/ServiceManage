using ServiceManagement.Models;

namespace ServiceManagement.Services
{
    public interface IEmailService
    {
        Task SendNewTaskEmail(UserEmailOptions userEmailOptions);

        Task SendForgetPasswordEmail(UserEmailOptions userEmailOptions);

        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}