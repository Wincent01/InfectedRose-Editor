namespace Behaviors
{
    public class Skill
    {
        public uint Cost { get; set; }
        
        public float Cooldown { get; set; }
        
        public uint CooldownGroup { get; set; }
        
        public uint Icon { get; set; }
        
        public BehaviorBase Root { get; set; }
    }
}