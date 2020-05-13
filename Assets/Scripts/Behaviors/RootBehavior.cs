namespace Behaviors
{
    [Behavior(Template.Root)]
    public class RootBehavior : BehaviorBase
    {
        [Parameter("Cost")]
        public uint Cost { get; set; }
        
        [Parameter("Cooldown")]
        public float Cooldown { get; set; }
        
        [Parameter("Cooldown Group")]
        public uint CooldownGroup { get; set; }
        
        [Parameter("Icon")]
        public uint Icon { get; set; }
        
        [Parameter("Next")]
        public BehaviorBase Next { get; set; }
    }
}