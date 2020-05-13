namespace Behaviors
{
    [Behavior(Template.MovementSwitch)]
    public class MovementSwitchBehavior : BehaviorBase
    {
        [Parameter("double_jump_action", "Double Jump")]
        public BehaviorBase DoubleJumpAction { get; set; }
        
        [Parameter("falling_action", "Fall")]
        public BehaviorBase FallingAction { get; set; }
        
        [Parameter("ground_action", "Ground")]
        public BehaviorBase GroundAction { get; set; }
        
        [Parameter("jetpack_action", "Jetpack")]
        public BehaviorBase JetpackAction { get; set; }
        
        [Parameter("jump_action", "Jump")]
        public BehaviorBase JumpAction { get; set; }
    }
}