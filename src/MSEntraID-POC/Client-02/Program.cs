using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

var adTenantId = "b046897f-c12f-49a4-9d20-65b78e454376";

// TestClient01 Application Id
var adClientId = "d76c2277-f2c0-45ed-a242-b0ae5da25b77";
var value = "C3U8Q~qJxaD2vtHXn0zUG90W6~Dr~dtHet8m1a61";


// TODO: Get Jwt Token from Azure AD

var tokenClient = new HttpClient();
var contentType = "application/x-www-form-urlencoded";

// Convert pem certificate to pfx
// openssl pkcs12 -inkey bob_key.pem -in bob_cert.cert -export -out bob_pfx.pfx

var certificatePath = "Certs/orig/client02.pfx";
var certificatePasword = ".surround123";

var certificate = new X509Certificate2(certificatePath, certificatePasword);
var credential = new ClientCertificateCredential(adTenantId, adClientId, certificate);
var token = credential.GetTokenAsync(
    new Azure.Core.TokenRequestContext(["api://907e3ef0-2849-4f68-8e26-e4d6bc204713/.default"])
    ).Result;

Console.WriteLine(token);
Console.WriteLine(token.Token);

tokenClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

var tokenString = token.Token;

// Call API with Bearer Token that came from MS Entra ID

var apiUrl = "https://localhost:7212/weatherforecast/";

Console.WriteLine("Calling API: " + apiUrl);

var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

var response = await client.GetAsync(apiUrl);
response.EnsureSuccessStatusCode();
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);
