using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

var adTenantId = "<MY_AD_TENANT_ID>";
var adClientId = "<MY_CLIENT_02_APP_ID>";

var apiClientId = "<MY_WEATHER_API_SERVICE_APP_ID>";

// Convert pem certificate to pfx
// openssl pkcs12 -inkey key.pem -in cert.pem -export -out client02.pfx

var certificatePath = "Certs/orig/client02.pfx";
var certificatePasword = "password12345";

var certificate = new X509Certificate2(certificatePath, certificatePasword);
var credential = new ClientCertificateCredential(adTenantId, adClientId, certificate);
var token = credential.GetTokenAsync(
    new Azure.Core.TokenRequestContext([$"api://{apiClientId}/.default"])
    ).Result;

Console.WriteLine(token.Token);


// Call API with Bearer Token that came from MS Entra ID
var apiUrl = "https://localhost:7212/weatherforecast/";

Console.WriteLine($"Calling API: {apiUrl}");

var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

var response = await client.GetAsync(apiUrl);
response.EnsureSuccessStatusCode();
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);
