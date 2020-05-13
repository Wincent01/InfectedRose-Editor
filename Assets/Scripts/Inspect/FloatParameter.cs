namespace Inspect
{
    public class FloatParameter : InspectParameter
    {
        public override bool TryParse(string text, out object result)
        {
            var success = float.TryParse(text, out var value);

            result = value;

            return success;
        }
    }
}