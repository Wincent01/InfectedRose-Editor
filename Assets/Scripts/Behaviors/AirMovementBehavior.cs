using Behaviors.External;
using UnityEngine;

namespace Behaviors
{
    [Behavior(Template.AirMovement)]
    public class AirMovementBehavior : BehaviorBase
    {
        [Parameter("goto_target")]
        public bool GotoTarget { get; set; }
        
        [Parameter("gravity_scale")]
        public float GravityScale { get; set; }
        
        [Parameter("ground_action", "Ground")]
        public BehaviorBase GroundAction { get; set; }
        
        [Parameter("hit_action", "Hit")]
        public BehaviorBase HitAction { get; set; }
        
        [Parameter("hit_action_enemy", "Enemy")]
        public BehaviorBase HitActionEnemy { get; set; }
        
        [Parameter("timeout_action", "Timeout")]
        public BehaviorBase TimeoutAction { get; set; }
        
        [Parameter("timeout_ms")]
        public uint TimeoutMs { get; set; }
        
        [Parameter("velocity")]
        public Vector3 Velocity { get; set; }

        public override void Export(BehaviorXml details)
        {
            details.SetParameter("x_velocity", Velocity.x);
            
            details.SetParameter("y_velocity", Velocity.y);
            
            details.SetParameter("z_velocity", Velocity.z);
        }
    }
}