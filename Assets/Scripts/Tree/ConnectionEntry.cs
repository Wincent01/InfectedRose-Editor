using System.Reflection;
using UnityEngine;

namespace Tree
{
    public class ConnectionEntry
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ConnectionSide Side { get; set; }

        public bool Entry { get; set; }

        public bool Enabled { get; set; }

        public Vector2 Offset { get; set; }
        
        public PropertyInfo Property { get; set; }
    }
}