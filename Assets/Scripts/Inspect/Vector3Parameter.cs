using System.Globalization;
using System.Reflection;
using Behaviors;
using TMPro;
using UnityEngine;

namespace Inspect
{
    public class Vector3Parameter : InspectEmpty
    {
        [SerializeField] protected TextMeshProUGUI _parameterName;

        [SerializeField] protected TMP_InputField _inputX;
        
        [SerializeField] protected TMP_InputField _inputY;
        
        [SerializeField] protected TMP_InputField _inputZ;
        
        public Vector3 Value
        {
            get => (Vector3) Info.GetValue(Instance);
            set => Info.SetValue(Instance, value);
        }

        public string Name
        {
            get => _parameterName.text;
            set => _parameterName.text = value;
        }

        private void Awake()
        {
            _inputX.onEndEdit.AddListener(raw =>
            {
                if (Info == null) return;
                
                var success = float.TryParse(raw, out var result);
                
                if (!success) return;

                var value = Value;

                value.x = result;

                Value = value;
            });
            
            _inputY.onEndEdit.AddListener(raw =>
            {
                if (Info == null) return;
                
                var success = float.TryParse(raw, out var result);
                
                if (!success) return;

                var value = Value;

                value.y = result;

                Value = value;
            });
            
            _inputZ.onEndEdit.AddListener(raw =>
            {
                if (Info == null) return;
                
                var success = float.TryParse(raw, out var result);
                
                if (!success) return;

                var value = Value;

                value.z = result;

                Value = value;
            });
        }

        public override void SetupParameter(PropertyInfo info, object instance)
        {
            base.SetupParameter(info, instance);
            
            Name = info.Name;

            var value = Value;

            _inputX.text = value.x.ToString(CultureInfo.InvariantCulture);

            _inputY.text = value.y.ToString(CultureInfo.InvariantCulture);

            _inputZ.text = value.z.ToString(CultureInfo.InvariantCulture);
        }
    }
}