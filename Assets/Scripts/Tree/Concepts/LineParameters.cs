using Behaviors;

namespace Tree.Concepts
{
    public class LineParameters
    {
        [Parameter("delay")]
        public float Delay { get; set; }
        
        [Parameter("duration")]
        public float Duration { get; set; }
        
        [Parameter("num_intervals")]
        public uint Intervals { get; set; }
        
        [Parameter("ignore_interrupts")]
        public bool IgnoreInterrupts { get; set; }
    }
}