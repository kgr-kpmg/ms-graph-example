using Azure.Identity;
using Microsoft.Graph;

// Multi-tenant apps can use "common",
// single-tenant apps must use the tenant ID from the Azure portal
var tenantId = "<your tenant id>";

// Value from app registration
var clientId = "<your client id>";

// using Azure.Identity;
var options = new InteractiveBrowserCredentialOptions
{
    TenantId = tenantId,
    ClientId = clientId,
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
    RedirectUri = new Uri("http://localhost"),
};

var interactiveCredential = new InteractiveBrowserCredential(options);

var graphClient = new GraphServiceClient(interactiveCredential, new[] { "Sites.Read.All" });

var sites = await graphClient.Sites
    .Request(new List<QueryOption> 
    {
        new QueryOption("?search", "*") 
    })
    .GetAsync();

Console.WriteLine($"Sites found: {sites.Count}");
Console.WriteLine("--------------");

foreach (var site in sites)
{
    Console.WriteLine($"Site Name: {site.DisplayName}");
    Console.WriteLine($"Site Description: {site.Description}");
    Console.WriteLine($"Site URL: {site.WebUrl}");
    Console.WriteLine("--------------------------------------------------------");
}
