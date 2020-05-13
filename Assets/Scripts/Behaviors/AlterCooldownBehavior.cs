namespace Behaviors
{
    [Behavior(Template.AlterCooldown)]
    public class AlterCooldownBehavior : BehaviorBase
    {
        [Parameter("add")]
        public bool Add { get; set; }
        
        [Parameter("amount")]
        public float Amount { get; set; }
    }
}