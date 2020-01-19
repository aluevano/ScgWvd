using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCGWvd.Models
{
    public class ShutdownNotification
    {
        public string skipUrl { get; set; }
        public string delayUrl60 { get; set; }
        public string delayUrl120 { get; set; }
        public string vmName { get; set; }
        public Guid guid { get; set; }
        public string owner { get; set; }
        public string vmUrl { get; set; }
        public string minutesUntilShutdown { get; set; }
        public string eventType { get; set; }
        public string text { get; set; }
        public Guid subscriptionId { get; set; }
        public string resourceGroupName { get; set; }
        public string labName { get; set; }


    }
}
