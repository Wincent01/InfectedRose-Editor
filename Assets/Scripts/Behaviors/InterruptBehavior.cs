namespace Behaviors
{
    [Behavior(Template.Interrupt)]
    public class InterruptBehavior : BehaviorBase
    {
        [Parameter("interrupt_attack")]
        public bool InterruptAttack { get; set; }
        
        [Parameter("interrupt_block")]
        public bool InterruptBlock { get; set; }
        
        [Parameter("interrupt_charge")]
        public bool InterruptCharge { get; set; }
        
        [Parameter("target")]
        public bool Target { get; set; }
    }
}