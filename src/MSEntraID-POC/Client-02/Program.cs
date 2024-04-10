using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

var adTenantId = "b046897f-c12f-49a4-9d20-65b78e454376";
var adClientId = "5c093831-44f2-4b7c-b5f2-4b4843b0d69d";

var apiClientId = "6af76b0a-bb24-498b-bc62-9e5433059f7d";

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
