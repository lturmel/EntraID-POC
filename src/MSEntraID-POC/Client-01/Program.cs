var adTenantId = "<MY_AD_TENANT_ID>";

var adClientId = "<MY_CLIENT_01_APP_ID>";
var value = "<MY_APP_SECRET_VALUE>";

var apiClientId = "<MY_WEATHER_API_SERVICE_APP_ID>";

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
