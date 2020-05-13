using Behaviors.External;

namespace Behaviors
{
    [Behavior(Template.Chain)]
    public class ChainBehavior : BehaviorBase
    {
        [Parameter("chain_delay")]
        public float ChainDelay { get; set; }
        
        public override void Export(BehaviorXml details)
        {
            for (var index = 0; index < Branches.Count; index++)
            {
                var branch = Branches[index];

                details.SetParameter($"behavior {index + 1}", branch);
            }
        }
    }
}