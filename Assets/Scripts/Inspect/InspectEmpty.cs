using System.Reflection;
using Behaviors;
using UnityEngine;

namespace Inspect
{
    public abstract class InspectEmpty : MonoBehaviour
    {
        public PropertyInfo Info { get; set; }
        
        public object Instance { get; set; }

        public virtual void SetupParameter(PropertyInfo info, object instance)
        {
            Info = info;
            Instance = instance;
        }
    }
}