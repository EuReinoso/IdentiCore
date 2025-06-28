namespace Web.Integration;

public class AuthHeaderHandler : DelegatingHandler
{
    public AuthHeaderHandler()
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = TokenProvider.Token;

        if (!string.IsNullOrEmpty(accessToken))
        {
            if (!accessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                accessToken = $"Bearer {accessToken}";

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
        }

        return base.SendAsync(request, cancellationToken);
    }
}