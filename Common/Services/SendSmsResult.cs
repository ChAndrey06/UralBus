namespace Common.Services;

public enum SendSmsResult
{
    Success,
    FailedInvalidNumber,
    FailedServiceError,
    FailedUnknown
}