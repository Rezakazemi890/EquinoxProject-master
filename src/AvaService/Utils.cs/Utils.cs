using System.Net.Http;
using Sample.AvaServices.Enum;

namespace Sample.Utils.cs;

public static class Utils
{

    public static string GetMustSaveProperties(string response, string request)
    {
        string mustProperties = "";
        foreach (string prop in Enum.GetNames(typeof(MustSaveProperties)))
        {
            if (response.Contains(prop) && !mustProperties.Contains(prop))
            {
                mustProperties += "," + prop;
            }
            if (request.Contains(prop) && !mustProperties.Contains(prop))
                mustProperties += "," + prop;
        }
        return mustProperties;
    }

    public static async Task<string> GetStatusLog(string response, HttpResponseMessage responseMessage, string mustSaveProperties, DateTime from, DateTime to)
    {
        string sep = "----------------------------------------------------------------------------------------------------";
        string log = string.Format("{8} Service {0}Request : {0}Url: {1}{0}Body: {2}{0}Response : {0}Body: {3}{0}StatusCode: {4} {0}ResponseTime: {6} {0}Must save : {7}{0}{5}{0}"
        , Environment.NewLine
        , responseMessage.RequestMessage.RequestUri,
        (await responseMessage.RequestMessage.Content.ReadAsStringAsync())
        , response
        , (int)(responseMessage.StatusCode)
        , sep
        , (to - from)
        , mustSaveProperties,
        responseMessage.RequestMessage.RequestUri.Segments.Last());
        return log;
    }
}
