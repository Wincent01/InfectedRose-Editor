using System.Reflection;
using Behaviors;
using TMPro;
using UnityEngine;

namespace Inspect
{
    public abstract class InspectParameter : InspectEmpty
    {
        [SerializeField] protected TextMeshProUGUI _parameterName;

        [SerializeField] protected TMP_InputField _input;
        
        public object Value
        {
            get => Info.GetValue(Instance);
            set => Info.SetValue(Instance, value);
        }

        public string Raw
        {
            get => _input.text;
            set => _input.text = value;
        }

        public string Name
        {
            get => _parameterName.text;
            set => _parameterName.text = value;
        }

        private void Awake()
        {
            _input.onEndEdit.AddListener(raw =>
            {
                if (Info == null) return;
                
                var success = TryParse(raw, out var result);
                
                if (!success) return;

                Value = result;
            });
        }

        public override void SetupParameter(PropertyInfo info, BehaviorBase instance)
        {
            base.SetupParameter(info, instance);
            
            Name = info.Name;
            Raw = Value.ToString();
        }

        public abstract bool TryParse(string text, out object result);
    }
}