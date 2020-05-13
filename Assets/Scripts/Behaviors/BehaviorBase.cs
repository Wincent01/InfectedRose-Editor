using System.Collections.Generic;
using Behaviors.External;

namespace Behaviors
{
    public abstract class BehaviorBase
    {
        [Parameter("Effect")]
        public uint Effect { get; set; }
        
        public List<BehaviorBase> Branches { get; set; }

        public BehaviorBase()
        {
            Branches = new List<BehaviorBase>();
        }

        public virtual void Export(BehaviorXml details)
        {
        }
    }
}