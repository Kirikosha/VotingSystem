using System.Text;

public interface IJsonParser
{
    public Task<object?> ParseJsonString(Microsoft.AspNetCore.Http.HttpRequest request, Encoding encoding);
}