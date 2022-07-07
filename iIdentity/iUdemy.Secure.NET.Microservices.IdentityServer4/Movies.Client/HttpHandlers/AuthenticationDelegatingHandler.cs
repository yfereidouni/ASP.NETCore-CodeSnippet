using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{

    //private readonly IHttpClientFactory _httpClientFactory;
    //private readonly ClientCredentialsTokenRequest _tokenRequest;

    //public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
    //{
    //    _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    //    _tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
    //}


    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //// Connect to IdentityServer
        //var httpClient = _httpClientFactory.CreateClient("IDPClient");

        //// Get Token
        //var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest);
        //if (tokenResponse.IsError)
        //{
        //    throw new HttpRequestException("Something went wrong while requesting the access token");
        //}

        //request.SetBearerToken(tokenResponse.AccessToken);

        //return await base.SendAsync(request, cancellationToken);


        //----------------------------------------------------------------
        var accessToken = await _httpContextAccessor
            .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.SetBearerToken(accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
