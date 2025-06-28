using Application.DTOs;

namespace Web.Integration;

public class IdentiCoreIntegration
{
    private readonly HttpClient _http;

    public IdentiCoreIntegration(HttpClient http)
    {
        _http = http;
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
            throw new ApplicationException(error?["error"] ?? "Failed to create client");
        }
    }

    public async Task DeleteClientAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"Clients/{id}");
        response.EnsureSuccessStatusCode();
    }
}
