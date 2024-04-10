var adTenantId = "b046897f-c12f-49a4-9d20-65b78e454376";

var adClientId = "b185aff9-1064-4d09-ba2d-ad64be3831f0";
var value = "Du08Q~n0PENt7CJvH6IgkdS-s96efqAYr7TRTbp3";

var apiClientId = "6af76b0a-bb24-498b-bc62-9e5433059f7d";

var credential = new Azure.Identity.ClientSecretCredential(adTenantId, adClientId, value);
var token = credential.GetTokenAsync(new Azure.Core.TokenRequestContext([$"api://{apiClientId}/.default"])).Result;
Console.WriteLine(token.Token);

// Call API with Bearer Token that came from MS Entra ID
var tokenString = token.Token;
var apiUrl = "https://localhost:7212/weatherforecast/";
Console.WriteLine($"Calling API: {apiUrl}");

var client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenString}");

var response = await client.GetAsync(apiUrl);

var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);
