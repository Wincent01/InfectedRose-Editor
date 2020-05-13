using System;

namespace Behaviors
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute
    {
        public string Name { get; }
        
        public string Display { get; }
        
        public ParameterAttribute(string name, string display = default)
        {
            Name = name;

            if (string.IsNullOrWhiteSpace(display))
            {
                display = name;
            }

            Display = display;
        }
    }
}