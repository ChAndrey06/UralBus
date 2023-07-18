namespace Common.Services;

public class MockSmsService : ISmsService
{
    public async Task<SendSmsResult> SendSmsAsync(string phoneNumber, string message)
    {
        // Here you can implement the mock logic for sending the SMS
        // and return the appropriate result based on the outcome
        if (phoneNumber.Length < 10)
        {
            return SendSmsResult.FailedInvalidNumber;
        }
        else if (phoneNumber.StartsWith("+1"))
        {
            return SendSmsResult.FailedServiceError;
        }
        else
        {
            return SendSmsResult.Success;
        }
    }
}