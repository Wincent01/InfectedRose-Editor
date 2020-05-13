namespace Behaviors
{
    [Behavior(Template.AttackDelay)]
    public class AttackDelayBehavior : BehaviorBase
    {
        [Parameter("action", "Next")]
        public BehaviorBase Action { get; set; }
        
        [Parameter("delay")]
        public float Delay { get; set; }
        
        [Parameter("ignore_interrupts")]
        public bool IgnoreInterrupts { get; set; }
        
        [Parameter("num_intervals")]
        public uint Intervals { get; set; }
    }
}