namespace Inspect
{
    public class UIntParameter : InspectParameter
    {
        public override bool TryParse(string text, out object result)
        {
            var success = uint.TryParse(text, out var value);

            result = value;

            return success;
        }
    }
}