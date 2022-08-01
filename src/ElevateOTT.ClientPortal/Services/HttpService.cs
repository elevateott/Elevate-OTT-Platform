namespace ElevateOTT.ClientPortal.Services;

public class HttpService : IHttpService
{
    #region Private Fields

    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    #endregion Private Fields

    #region Public Constructors

    public HttpService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    #endregion Public Constructors

    #region Private Properties

    private static JsonSerializerOptions DefaultJsonSerializerOptions => new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    #endregion Private Properties

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> Get<TResponse>(string url)
    {
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        using var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Get<TRequest, TResponse>(string url, TRequest data)
    {
        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url),
            Content = stringContent,
        };

        using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Post<TRequest, TResponse>(string url, TRequest data)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PostAsync(url, stringContent);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> PostFormData<TRequest, TResponse>(string url, MultipartFormDataContent data)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        Console.WriteLine("base uri: " + _httpClient.BaseAddress);

        using var response = await _httpClient.PostAsync(url, data).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions).ConfigureAwait(false);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Post<TResponse>(string url)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        using var response = await _httpClient.PostAsync(url, null);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Put<TRequest, TResponse>(string url, TRequest data)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        var dataJson = JsonSerializer.Serialize(data);

        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PutAsync(url, stringContent);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Delete<TResponse>(string url)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        using var response = await _httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<SuccessResult<TResponse>>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, true, response);
        }
        else
        {
            var responseDeserialized = await Deserialize<ExceptionResult>(response, DefaultJsonSerializerOptions);
            return new HttpResponseWrapper<object>(responseDeserialized, false, response);
        }
    }

    public async Task<HttpResponseWrapper<object>> Delete(string url)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", await _localStorage.GetItemAsync<string>("Culture"));

        var responseHttp = await _httpClient.DeleteAsync(url);

        return new HttpResponseWrapper<object>(null, responseHttp.IsSuccessStatusCode, responseHttp);
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
    {
        var responseString = await httpResponse.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseString, options);
    }

    #endregion Private Methods
}
