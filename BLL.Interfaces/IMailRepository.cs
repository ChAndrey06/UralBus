using BLL.Entities.Mail;

namespace BLL.Interfaces
{
    public interface IMailRepository
    {
        Task Send(Mail mail);
    }
}
