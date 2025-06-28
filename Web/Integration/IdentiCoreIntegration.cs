using Application.DTOs;

namespace Web.Integration;

public class IdentiCoreIntegration
{
    private readonly HttpClient _http;

    public IdentiCoreIntegration(HttpClient http)
    {
        _http = http;
    }

    public async Task Login(AuthDto dto)
    {
        var response = await _http.PostAsJsonAsync("Auth/Login", dto);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Login failed");

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

        var token = result?.Token;

        if (string.IsNullOrEmpty(token))
            throw new ApplicationException("Empty Token");

        TokenProvider.Token = token;
    }

    public async Task<IEnumerable<DetailClientDto>> GetAllClientsAsync()
    {
        var response = await _http.GetAsync("Clients");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<DetailClientDto>>();
    }

    public async Task<DetailClientDto?> GetClientByIdAsync(Guid id)
    {
        var response = await _http.GetAsync($"Clients/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<DetailClientDto>()
            : null;
    }

    public async Task CreateClientAsync(UpdateClientDto dto)
    {
        var response = await _http.PostAsJsonAsync("Clients", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new ApplicationException(error?["error"] ?? "Failed to create client");
        }
    }

    public async Task UpdateClientAsync(Guid id, UpdateClientDto dto)
    {
        var response = await _http.PutAsJsonAsync($"Clients/{id}", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new ApplicationException(error?["error"] ?? "Failed to update client");
        }
    }

    public async Task DeleteClientAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"Clients/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<DetailAddressDto?> GetAddressByIdAsync(Guid id)
    {
        var response = await _http.GetAsync($"Addresses/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<DetailAddressDto>()
            : null;
    }

    public async Task<IEnumerable<DetailAddressDto>?> GetAddressByClientIdAsync(Guid id)
    {
        var response = await _http.GetAsync($"Addresses/client/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<IEnumerable<DetailAddressDto>?>()
            : null;
    }

    public async Task CreateAddressAsync(CreateAddressDto dto)
    {
        var response = await _http.PostAsJsonAsync("Addresses", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            throw new ApplicationException(error?["error"].ToString() ?? "Failed to create address");
        }
    }

    public async Task UpdateAddressAsync(Guid id, UpdateAddressDto dto)
    {
        var response = await _http.PutAsJsonAsync($"Addresses/{id}", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            throw new ApplicationException(error?["error"].ToString() ?? "Failed to update address");
        }
    }

    public async Task DeleteAddressAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"Addresses/{id}");
        response.EnsureSuccessStatusCode();
    }
}
