using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;

namespace SCGWvd.Models.Interfaces
{
    public interface IScgVm
    {
        string ResourceGroup { get; }
        string Id { get; }
        string ComputerName { get; }
        string CurrentState { get; }
        IList<IScgVm> DependsOn { get; }
    }

    public static class ScgVmExtensions
    {
        public static IScgVm CreateScgVm(this IVirtualMachine vm)
        {
            return new ScgVm(vm.Id,vm.ComputerName,vm.PowerState.Value,vm.ResourceGroupName);
        }

        public static IScgVm AddDependentVm(this IScgVm scgVm, IScgVm dependencyName)
        {
            scgVm.DependsOn.Add(dependencyName);
            return scgVm;
        }

        public static IScgVm PopulateDependentVms(this IScgVm scgVm, IVirtualMachine azureVm, IAzure azure)
        {
            var machineDependsOn = azureVm.Tags.Where((pair, i) =>
                    pair.Key.Equals("DependsOn", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().Value?.Split(",");

            machineDependsOn?.AsParallel().ForAll(async id => scgVm.AddDependentVm((await azure.VirtualMachines.GetByIdAsync(id)).CreateScgVm()));

            return scgVm;
        }

    }
}