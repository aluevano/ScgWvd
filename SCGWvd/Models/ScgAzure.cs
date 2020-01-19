using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.BatchAI.Fluent;
using Microsoft.Azure.Management.Cdn.Fluent;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.ContainerInstance.Fluent;
using Microsoft.Azure.Management.ContainerRegistry.Fluent;
using Microsoft.Azure.Management.ContainerService.Fluent;
using Microsoft.Azure.Management.CosmosDB.Fluent;
using Microsoft.Azure.Management.Dns.Fluent;
using Microsoft.Azure.Management.Eventhub.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.KeyVault.Fluent;
using Microsoft.Azure.Management.Locks.Fluent;
using Microsoft.Azure.Management.Monitor.Fluent;
using Microsoft.Azure.Management.Msi.Fluent;
using Microsoft.Azure.Management.Network.Fluent;
using Microsoft.Azure.Management.PrivateDns.Fluent;
using Microsoft.Azure.Management.Redis.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Search.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Storage.Fluent;
using Microsoft.Azure.Management.TrafficManager.Fluent;
using Microsoft.Rest.Azure;
using ISubscription = Microsoft.Azure.Management.ResourceManager.Fluent.ISubscription;
using ISubscriptions = Microsoft.Azure.Management.ResourceManager.Fluent.ISubscriptions;

namespace SCGWvd.Models
{
    public class ScgAzure: IAzure
    {
        public INetworkWatchers NetworkWatchers { get; }
        public IVirtualNetworkGateways VirtualNetworkGateways { get; }
        public ILocalNetworkGateways LocalNetworkGateways { get; }
        public IExpressRouteCircuits ExpressRouteCircuits { get; }
        public IApplicationSecurityGroups ApplicationSecurityGroups { get; }
        public IRouteFilters RouteFilters { get; }
        public IDdosProtectionPlans DdosProtectionPlans { get; }
        public IWebApps WebApps { get; }
        public IAppServiceManager AppServices { get; }
        public ISearchServices SearchServices { get; }
        public IContainerServices ContainerServices { get; }
        public IKubernetesClusters KubernetesClusters { get; }
        public ICosmosDBAccounts CosmosDBAccounts { get; }
        public IContainerGroups ContainerGroups { get; }
        public IRegistries ContainerRegistries { get; }
        public IManagementLocks ManagementLocks { get; }
        public IIdentities Identities { get; }
        public IBatchAIWorkspaces BatchAIWorkspaces { get; }
        public IBatchAIUsages BatchAIUsages { get; }
        public IActivityLogs ActivityLogs { get; }
        public IMetricDefinitions MetricDefinitions { get; }
        public IDiagnosticSettings DiagnosticSettings { get; }
        public IActionGroups ActionGroups { get; }
        public IGalleries Galleries { get; }
        public IGalleryImages GalleryImages { get; }
        public IGalleryImageVersions GalleryImageVersions { get; }
        public ISubscription GetCurrentSubscription()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAzureClient> ManagementClients { get; }
        public string SubscriptionId { get; }
        public IServiceBusNamespaces ServiceBusNamespaces { get; }
        public ILoadBalancers LoadBalancers { get; }
        public IAccessManagement AccessManagement { get; }
        public IApplicationGateways ApplicationGateways { get; }
        public ISubscriptions Subscriptions { get; }
        public IResourceGroups ResourceGroups { get; }
        public IStorageAccounts StorageAccounts { get; }
        public IVirtualMachines VirtualMachines { get; }
        public IVirtualMachineScaleSets VirtualMachineScaleSets { get; }
        public INetworks Networks { get; }
        public INetworkSecurityGroups NetworkSecurityGroups { get; }
        public IPublicIPAddresses PublicIPAddresses { get; }
        public INetworkInterfaces NetworkInterfaces { get; }
        public IDeployments Deployments { get; }
        public IPolicyAssignments PolicyAssignments { get; }
        public IPolicyDefinitions PolicyDefinitions { get; }
        public IVirtualMachineImages VirtualMachineImages { get; }
        public IVirtualMachineExtensionImages VirtualMachineExtensionImages { get; }
        public IAvailabilitySets AvailabilitySets { get; }
        public IVaults Vaults { get; }
        public ITrafficManagerProfiles TrafficManagerProfiles { get; }
        public IDnsZones DnsZones { get; }
        public IPrivateDnsZones PrivateDnsZones { get; }
        public ISqlServers SqlServers { get; }
        public IRedisCaches RedisCaches { get; }
        public ICdnProfiles CdnProfiles { get; }
        public IVirtualMachineCustomImages VirtualMachineCustomImages { get; }
        public IDisks Disks { get; }
        public ISnapshots Snapshots { get; }
        public IComputeSkus ComputeSkus { get; }
        public IEventHubNamespaces EventHubNamespaces { get; }
        public IEventHubs EventHubs { get; }
        public IEventHubDisasterRecoveryPairings EventHubDisasterRecoveryPairings { get; }
        public IAlertRules AlertRules { get; }
        public IAutoscaleSettings AutoscaleSettings { get; }
        public IRegistryTasks ContainerRegistryTasks { get; }
        public IRegistryTaskRuns RegistryTaskRuns { get; }
    }
}
