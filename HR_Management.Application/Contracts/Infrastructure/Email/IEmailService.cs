namespace ERP.Application.Interfaces.Email;

public interface IEmailService
{
    Task<bool> SendEmail(EmailDto email);
}