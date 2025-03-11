using Microsoft.Identity.Client;
using System.Net.Http.Headers;

HttpClient client = new HttpClient();

var clientId = "bf36fc04-d9ea-4777-b56e-1bdef0802c40";
var clientSecret = "qdE8Q~YhKcSTZU4TiYLZsrdH-KeZZJ98mVBUcawR";
var scopes = new[] { "api://aee2fa2c-b5ae-4a46-88f8-24bbff1f790b/.default"};

var authority = $"https://login.microsoftonline.com/674497ca-6f67-465d-87a8-fe8034cd949e/oauth2/v2.0/";

var app = ConfidentialClientApplicationBuilder
    .Create(clientId)
    .WithAuthority(authority)
    .WithClientSecret(clientSecret)
    .Build();

var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
Console.WriteLine($"Access Token: {result.AccessToken}");

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
var response = await client.GetAsync("http://localhost:5000/weatherforecast");
var content = await response.Content.ReadAsStringAsync();

Console.WriteLine("Your response is: " + response.StatusCode);
Console.WriteLine(content);