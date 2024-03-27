﻿using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

//var adInstance = "https://login.microsoftonline.com/";
//var adDomain = "expertasolutions.com";
var adTenantId = "b046897f-c12f-49a4-9d20-65b78e454376";

// TestClient01 Application Id
var adClientId = "d76c2277-f2c0-45ed-a242-b0ae5da25b77";
//var secretId = "addd86b7-0d75-4de7-a8d5-6c1175f5b241";
var value = "C3U8Q~qJxaD2vtHXn0zUG90W6~Dr~dtHet8m1a61";

// TODO: Get Jwt Token from Azure AD
var authUrl = $"https://login.microsoftonline.com/{adTenantId}/oauth2/token";
var tokenClient = new HttpClient();
var contentType = "application/x-www-form-urlencoded";

tokenClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

// See: https://goodworkaround.com/2020/07/07/authenticating-to-azure-ad-as-an-application-using-certificate-based-client-credential-grant/

var jwtHeader = new {
    alg = "RS256",
    typ = "JWT",
    x5t = "C3U8Q~qJxaD2vtHXn0zUG90W6~Dr~dtHet8m1a61"
};

var jwtPayload = new {
    aud = authUrl,
    exp = DateTime.Now.AddMinutes(5).Ticks,
    iss = adClientId,
    jti = Guid.NewGuid().ToString(),
    nbf = DateTime.Now.Ticks,
    sub = adClientId,
};
var jwtSignature = $"{JsonConvert.SerializeObject(jwtHeader)}.{JsonConvert.SerializeObject(jwtPayload)}";
var hash = new HMACSHA256(Encoding.UTF8.GetBytes(jwtSignature));
var signature = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
var jwt = $"{jwtSignature}.{Convert.ToBase64String(signature)}";

var authContent = new List<KeyValuePair<string, string>> {
    new ("client_id", adClientId),
    new ("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"),
    new ("client_assertion", jwt),
    new ("grant_type", "client_credentials"),
    new ("scopes", "api://907e3ef0-2849-4f68-8e26-e4d6bc204713/.default"),
};

var result = await tokenClient.PostAsync(authUrl, new FormUrlEncodedContent(authContent));

var token = JsonConvert.DeserializeObject<dynamic>(await result.Content.ReadAsStringAsync());
Console.WriteLine(token);


/*
var tokenString = token.access_token;

// Call API with Bearer Token that came from MS Entra ID

var apiUrl = "https://localhost:7212/weatherforecast/";

Console.WriteLine("Calling API: " + apiUrl);

var client = new HttpClient();
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

var response = await client.GetAsync(apiUrl);
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);
*/