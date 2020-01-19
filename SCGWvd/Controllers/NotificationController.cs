using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SCGWvd.Models;

namespace SCGWvd.Controllers
{
    [AllowAnonymous]
    public class NotificationController : Controller
    {
        private ILogger<NotificationController> Logger { get; }
        private static string TeamsPayload = "{     \"$schema\": \"http://adaptivecards.io/schemas/adaptive-card.json\",     \"type\": \"AdaptiveCard\",     \"version\": \"1.0\",     \"body\": [{             \"type\": \"ColumnSet\",             \"id\": \"1ede2aba-61b9-faa0-9895-9ed0c26b2e6f\",             \"columns\": [{                     \"type\": \"Column\",                     \"id\": \"e5756242-0963-37a2-7cb4-4397886d60bb\",                     \"padding\": \"None\",                     \"width\": \"stretch\",                     \"items\": [{                             \"type\": \"TextBlock\",                             \"id\": \"20f3833e-0435-5c87-fad1-b528e0046fb6\",                             \"text\": \"The Stratus Cloud Group\",                             \"wrap\": true,                             \"weight\": \"Bolder\",                             \"color\": \"Accent\"                         }                     ],                     \"verticalContentAlignment\": \"Center\"                 }, {                     \"type\": \"Column\",                     \"id\": \"74215a26-fa8b-e549-cced-7f99fd34a661\",                     \"padding\": \"None\",                     \"width\": \"auto\",                     \"items\": [{                             \"type\": \"Image\",                             \"id\": \"795047e2-e63e-6e14-07ba-5a3e13323dff\",                             \"url\": \"https://amdesigner.azurewebsites.net/samples/assets/PlaceHolder_Person.png\",                             \"size\": \"Small\",                             \"style\": \"Person\"                         }                     ],                     \"horizontalAlignment\": \"Right\"                 }             ],             \"padding\": {                 \"top\": \"Small\",                 \"bottom\": \"Small\",                 \"left\": \"Default\",                 \"right\": \"Small\"             },             \"style\": \"emphasis\"         }, {             \"type\": \"Container\",             \"id\": \"fbcee869-2754-287d-bb37-145a4ccd750b\",             \"padding\": \"Default\",             \"spacing\": \"None\",             \"items\": [{                     \"type\": \"TextBlock\",                     \"id\": \"44906797-222f-9fe2-0b7a-e3ee21c6e380\",                     \"text\": \"WVD Shutdown Warning\",                     \"wrap\": true,                     \"weight\": \"Bolder\",                     \"size\": \"Large\",                     \"color\": \"Warning\"                 }, {                     \"type\": \"TextBlock\",                     \"id\": \"f7abdf1a-3cce-2159-28ef-f2f362ec937e\",                     \"text\": \"{{vmname}} will shutdown in {{shutdownTime}}\",                     \"wrap\": true                 }, {                     \"type\": \"ActionSet\",                     \"id\": \"c8a5ba7e-7ec6-53ae-79da-0cfb952a527e\",                     \"actions\": [{                             \"type\": \"Action.OpenUrl\",                             \"id\": \"36bc7be2-fc01-aed6-472e-668a8623f8f9\",                             \"title\": \"PostPone 1 Hour\",                             \"url\": \"{{delay1HourUrl}}\"                         }, {                             \"type\": \"Action.OpenUrl\",                             \"id\": \"43eec8de-296a-aa96-4acd-3af359a572c6\",                             \"title\": \"Postpone 2 Hours\",                             \"url\": \"{{delay2HourUrl}}\"                         }, {                             \"type\": \"Action.OpenUrl\",                             \"id\": \"a1a26e39-ef34-9376-c28a-b88c5f6c112d\",                             \"title\": \"Skip Shutdown\",                             \"url\": \"{{skipInstance}}\"                         }                     ]                 }, {                     \"type\": \"ActionSet\",                     \"actions\": [{                             \"type\": \"Action.OpenUrl\",                             \"id\": \"46b72e00-1033-da44-cc10-d90403f9daca\",                             \"title\": \"Click here to start the Instance Again\",                             \"url\": \"{{startInstance}}\"                         }                     ]                 }             ]         }     ],     \"padding\": \"None\" } ";
        private static string TeamsPayload2 = "{     \"@context\": \"https://schema.org/extensions\",     \"@type\": \"MessageCard\",     \"themeColor\": \"0072C6\",     \"title\": \"**WVD Shutdown Warning**\",     \"text\": \"{{vmname}} will shutdown in {{shutdownTime}} minutes\",     \"potentialAction\": [         {             \"@type\": \"OpenUri\",             \"name\": \"Postpone 1 Hour\",             \"targets\": [                 {                     \"os\": \"default\",                     \"uri\": \"{{delay1HourUrl}}\"                 }             ]         },         {             \"@type\": \"OpenUri\",             \"name\": \"Postpone 2 Hours\",             \"targets\": [                 {                     \"os\": \"default\",                     \"uri\": \"{{delay2HourUrl}}\"                 }             ]         },         {             \"@type\": \"OpenUri\",             \"name\": \"Skip Shutdown\",             \"targets\": [                 {                     \"os\": \"default\",                     \"uri\": \"{{skipInstance}}\"                 }             ]         },         {             \"@type\": \"OpenUri\",             \"name\": \"Click here to start the instance again\",             \"targets\": [                 {                     \"os\": \"default\",                     \"uri\": \"{{startInstance}}\"                 }             ]         }     ] }";
        private const string TeamsWebhookUrl =
            "https://outlook.office.com/webhook/605a1c50-df10-4adb-9614-f66277727d54@0105c271-1a0c-4114-9883-f3f61c0e2217/IncomingWebhook/f8ade34f4e2043b4b598a71d41fd90cf/51b44b99-2118-43b3-a895-a86c0eeaf007";
            private readonly Uri TeamsWebHookUri = new Uri(TeamsWebhookUrl);
        public NotificationController(ILogger<NotificationController> logger)
        {
            Logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> IncomingFromAzureAutoShutdown([FromBody]ShutdownNotification data)
        {
            using (Logger.BeginScope(new Dictionary<string, string>() {{"test", "test"}}))
            {
                Logger.LogWarning($"Got shutdown notification {data.ToString()}");
                var payload = TeamsPayload2.Replace("{{vmname}}", data.vmName)
                    .Replace("{{shutdownTime}}", data.minutesUntilShutdown)
                    .Replace("{{delay1HourUrl}}", data.delayUrl60)
                    .Replace("{{delay2HourUrl}}", data.delayUrl120)
                    .Replace("{{skipInstance}}", data.skipUrl)
                    .Replace("{{startInstance}}", $"https://scgwvdtestapp.azurewebsites.net/Home/Connect?computerName={data.vmName}&resourceGroupName={data.resourceGroupName}");
                var client = new HttpClient();
                var postResult = await client.PostAsync(TeamsWebHookUri, new StringContent(payload));
                return new OkObjectResult(postResult.Content.ToString());
            }
        }

    }
}