using Behaviors.External;

namespace Behaviors
{
    [Behavior(Template.And)]
    public class AndBehavior : BehaviorBase
    {
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