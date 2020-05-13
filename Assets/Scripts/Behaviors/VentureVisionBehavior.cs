namespace Behaviors
{
    [Behavior(Template.VentureVision)]
    public class VentureVisionBehavior : BehaviorBase
    {
        [Parameter("show_collectibles")]
        public bool ShowCollectibles { get; set; }
        
        [Parameter("show_minibosses")]
        public bool ShowMiniBosses { get; set; }
        
        [Parameter("show_pet_digs")]
        public bool ShowPetDigs { get; set; }
    }
}