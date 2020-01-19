using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using SCGWvd.Models;
using SCGWvd.Models.Interfaces;

namespace SCGWvd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IAzure Azure { get; }
        private ITokenAcquisition TokenAcquisition { get; }
        public IOptions<WebOptions> WebOptionValue { get; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IAzure azure, ITokenAcquisition tokenAcquisition, IOptions<WebOptions> webOptionValue)
        {
            Azure = azure;
            TokenAcquisition = tokenAcquisition;
            WebOptionValue = webOptionValue;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //string result = await TokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(new[] { Constants.ScopeUserRead });
            return View(GetScgVms());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[MsalUiRequiredExceptionFilter(Scopes = new[] { "User.Read" })]
        public async Task<IActionResult> Test()
        {
            var token = await TokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(new[] { "https://mrs-prod.ame.gbl/mrs-RDInfra-prod/user_impersonation" });
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://rdweb.wvd.microsoft.com/");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/x-msts-radc-discovery+xml,text/xml");
            httpClient.DefaultRequestHeaders.Add("x-ms-correlation-id", "bc7a963a-3f8e-484f-adc9-f5f939e40000");
            httpClient.DefaultRequestHeaders.Add("X-MS-User-Agent", "com.microsoft.rdc.html/1.0.20.2");
            var result = await httpClient.GetAsync("api/hubdiscovery/eventhubdiscovery.aspx");
            result = await httpClient.GetAsync("api/feeddiscovery/webfeeddiscovery.aspx");
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> Connect(string computerName,string resourceGroupName)
        {
            PowerOn(computerName, resourceGroupName);
            await Task.Delay(TimeSpan.FromSeconds(10));
            //return Redirect("https://rdweb.wvd.microsoft.com/webclient/index.html");
            return new RedirectResult("https://rdweb.wvd.microsoft.com/webclient/index.html");
            //return new OkResult();
        }

        public async Task<IEnumerable<IScgVm>> GetScgVms()
        {
            var listOfRelevantVms = new List<IScgVm>();
            var vms = await Azure.VirtualMachines.ListAsync();
            vms.AsParallel().ForAll(vm =>
            {
                var scgVm = vm.CreateScgVm()
                    .PopulateDependentVms(vm, Azure);

                if(scgVm.ComputerName.StartsWith("SCGWvd"))
                    listOfRelevantVms.Add(scgVm);
            });

            return listOfRelevantVms;
        }

        private async Task PowerOn(string computerName, string resourceGroupName)
        {
            var azure = Azure;
            await azure.VirtualMachines.StartAsync(resourceGroupName, computerName);
            
        }
    }
}
