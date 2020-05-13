namespace Inspect
{
    public class IntParameter : InspectParameter
    {
        public override bool TryParse(string text, out object result)
        {
            var success = int.TryParse(text, out var value);

            result = value;

            return success;
        }
    }
}