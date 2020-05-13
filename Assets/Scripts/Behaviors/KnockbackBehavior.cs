namespace Behaviors
{
    [Behavior(Template.Knockback)]
    public class KnockbackBehavior : BehaviorBase
    {
        [Parameter("angle")]
        public float Angle { get; set; }
        
        [Parameter("caster")]
        public bool Caster { get; set; }
        
        [Parameter("ignore_self")]
        public bool IgnoreSelf { get; set; }
        
        [Parameter("relative")]
        public bool Relative { get; set; }
        
        [Parameter("strength")]
        public float Strength { get; set; }
    }
}