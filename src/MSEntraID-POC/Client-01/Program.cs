var adTenantId = "b046897f-c12f-49a4-9d20-65b78e454376";

// TestClient01 Application Id
var adClientId = "d76c2277-f2c0-45ed-a242-b0ae5da25b77";
var value = "C3U8Q~qJxaD2vtHXn0zUG90W6~Dr~dtHet8m1a61";

var credential = new Azure.Identity.ClientSecretCredential(adTenantId, adClientId, value);
var token = credential.GetTokenAsync(new Azure.Core.TokenRequestContext(["api://907e3ef0-2849-4f68-8e26-e4d6bc204713/.default"])).Result;

Console.WriteLine(token.Token);

var tokenString = token.Token;

// Call API with Bearer Token that came from MS Entra ID

var apiUrl = "https://localhost:7212/weatherforecast/";

Console.WriteLine("Calling API: " + apiUrl);

var client = new HttpClient();
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

var response = await client.GetAsync(apiUrl);
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);