using Behaviors.External;
using UnityEngine;

namespace Behaviors
{
    [Behavior(Template.Camera)]
    public class CameraBehavior : BehaviorBase
    {
        [Parameter("lock_camera")]
        public bool Lock { get; set; }
        
        [Parameter("lookat_relative")]
        public bool LookAtRelative { get; set; }
        
        [Parameter("pos_relative")]
        public bool PositionRelative { get; set; }
        
        [Parameter("lootat")]
        public Vector3 LookAt { get; set; }
        
        [Parameter("pos")]
        public Vector3 Position { get; set; }

        public override void Export(BehaviorXml details)
        {
            details.SetParameter("lookat_x", LookAt.x);
            
            details.SetParameter("lookat_y", LookAt.y);
            
            details.SetParameter("lookat_z", LookAt.z);
            
            details.SetParameter("pos_x", Position.x);
            
            details.SetParameter("pos_y", Position.y);
            
            details.SetParameter("pos_z", Position.z);
        }
    }
}