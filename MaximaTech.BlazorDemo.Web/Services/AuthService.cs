using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationsStateProvider;
    private readonly IConfiguration _configuration;

    public AuthService(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationsStateProvider,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authenticationsStateProvider = authenticationsStateProvider;
        _configuration = configuration;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync<LoginModel>(
            _configuration.GetConnectionString("authUrl"),
            loginModel,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        var loginResult = JsonSerializer.Deserialize<LoginResult>(
            await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (loginResult == null)
            return new LoginResult { Success = false };

        if (!response.IsSuccessStatusCode || loginResult.Data is null)
            return loginResult;

        await _localStorage.SetItemAsync<LoginData>(Constants.USER_DATA_LOCAL_STORAGE_KEY, loginResult.Data);
        ((ApiAuthenticationStateProvider)_authenticationsStateProvider).MarkUserAuthenticated(loginResult.Data.Usuario.Email);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Data.AccessToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(Constants.USER_DATA_LOCAL_STORAGE_KEY);
        ((ApiAuthenticationStateProvider)_authenticationsStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
