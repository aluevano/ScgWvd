using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCGWvd.Models.Interfaces;

namespace SCGWvd.Models
{
    public class ScgVm : IScgVm
    {
        public string ResourceGroup { get; }
        public string Id { get; }
        public string ComputerName { get; }
        public string CurrentState { get; }
        public IList<IScgVm> DependsOn { get; }

        public ScgVm(string id, string computerName, string currentState, string resourceGroup, IList<IScgVm> dependsOn = null)
        {
            Id = id;
            ComputerName = computerName;
            CurrentState = currentState;
            ResourceGroup = resourceGroup;
            DependsOn = dependsOn ?? new List<IScgVm>();
        }
    }
}
