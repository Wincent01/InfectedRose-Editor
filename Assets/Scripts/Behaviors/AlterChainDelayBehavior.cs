namespace Behaviors
{
    [Behavior(Template.AlterChainDelay)]
    public class AlterChainDelayBehavior : BehaviorBase
    {
        [Parameter("chain_action", "Chain")]
        public BehaviorBase ChainAction { get; set; }
        
        [Parameter("new_delay")]
        public float NewDelay { get; set; }
    }
}