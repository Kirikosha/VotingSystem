using System.Text;
using Newtonsoft.Json;

public class JsonParserVoting : IJsonParser
{
    public ParsedJsonResult? result {get; set;}
    public JsonParserVoting()
    {
        result = new ParsedJsonResult();
    }
    public async Task<object?> ParseJsonString(HttpRequest request, Encoding encoding)
    {
            string jsonString;

            //Parsing the body of request to get values
            using (var reader = new StreamReader(request.Body, encoding))
            {
                jsonString = await reader.ReadToEndAsync();
                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new ArgumentNullException(nameof(jsonString), "JSON can't be parsed");
                }

                result = JsonConvert.DeserializeObject<ParsedJsonResult>(jsonString) ?? null;
            }

            return result;
    }
}

public sealed class ParsedJsonResult
{
    public string name {get; set;} = new string("");
    public string[]? propositions {get; set;}
}