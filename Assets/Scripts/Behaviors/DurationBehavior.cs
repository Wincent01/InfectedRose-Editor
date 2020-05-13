namespace Behaviors
{
    [Behavior(Template.Duration)]
    public class DurationBehavior : BehaviorBase
    {
        [Parameter("action", "Next")]
        public BehaviorBase Action { get; set; }
        
        [Parameter("duration")]
        public float Duration { get; set; }
    }
}