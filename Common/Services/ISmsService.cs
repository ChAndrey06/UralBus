namespace Common.Services;

public interface ISmsService
{
    Task<SendSmsResult> SendSmsAsync(string phoneNumber, string message);
}