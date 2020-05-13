using System.Reflection;
using Behaviors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inspect
{
    public class BooleanParameter : InspectEmpty
    {
        [SerializeField] protected TextMeshProUGUI _parameterName;

        [SerializeField] private Button _button;

        [SerializeField] private TextMeshProUGUI _state;
        
        public bool Value
        {
            get => (bool) Info.GetValue(Instance);
            set => Info.SetValue(Instance, value);
        }

        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                Value = !Value;

                _state.text = Value ? "True" : "False";
            });
        }
        
        public override void SetupParameter(PropertyInfo info, BehaviorBase instance)
        {
            base.SetupParameter(info, instance);
            
            _parameterName.text = info.Name;
            _state.text = Value ? "True" : "False";
        }
    }
}