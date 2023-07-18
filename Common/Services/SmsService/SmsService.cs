using System.Net;
using Newtonsoft.Json;

namespace Common.Services.SmsService;

public class SmsApiResponse
{
    public List<SmsApiSendResult> Send { get; set; }
    public string Balance { get; set; }
    public string Cost { get; set; }
    public string ServerPacketId { get; set; }
    public SmsApiError Error { get; set; }
}

public class SmsApiSendResult
{
    public string ServerId { get; set; }
    public string Phone { get; set; }
    public string Price { get; set; }
    public string Status { get; set; }
}

public class SmsApiError
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string DescriptionRu { get; set; }
}
public class SmsService : ISmsService
{
    //@TODO: REMOVE HARDCODE
    private readonly string _apiKey="2B914A4LEENLWVB4U95Q1KE45YR73C01116XU51EG4U7VC41M12M96XXHNCWWOD8";
    private readonly string _senderName="INFORM";
    private readonly string _baseUrl = "http://smspilot.ru/api.php";
    
    public async Task<SendSmsResult> SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            var url = $"{_baseUrl}?send={Uri.EscapeUriString(message)}" +
                      $"&to={phoneNumber}" +
                      $"&from={_senderName}" +
                      $"&apikey={_apiKey}" +
                      $"&format=json";

            var response = await new HttpClient().GetAsync(url);

            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var result = await streamReader.ReadToEndAsync();
            var smsApiResponse = JsonConvert.DeserializeObject<SmsApiResponse>(result);

            if (smsApiResponse.Send != null && smsApiResponse.Send.Count > 0 &&
                smsApiResponse.Send[0].Status == "0")
            {
                return SendSmsResult.Success;
            }
            else if (smsApiResponse.Error != null && 
                     smsApiResponse.Error.Code == "111" && 
                     smsApiResponse.Error.DescriptionRu.Contains("неправильный номер"))
            {
                return SendSmsResult.FailedInvalidNumber;
            }
            else if (smsApiResponse.Error != null)
            {
                return SendSmsResult.FailedServiceError;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return SendSmsResult.FailedUnknown;
        }
        return SendSmsResult.FailedUnknown;
    }
}
