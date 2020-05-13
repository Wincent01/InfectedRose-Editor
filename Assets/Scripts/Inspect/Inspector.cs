using System.Collections.Generic;
using System.Reflection;
using Behaviors;
using TMPro;
using Tree;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inspect
{
    public class Inspector : MonoBehaviour
    {
        [FormerlySerializedAs("_name")] [SerializeField] private TextMeshProUGUI _inspectName;
        
        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private GameObject _inspector;

        [SerializeField] private RectTransform _body;

        [SerializeField] private GameObject _floatParameter;

        [SerializeField] private GameObject _intParameter;

        [SerializeField] private GameObject _uintParameter;
        
        [SerializeField] private GameObject _booleanParameter;
        
        [SerializeField] private GameObject _vectorParameter;
        
        private Vector2 OriginSize { get; set; }

        private List<GameObject> Parameters { get; }

        public Inspector()
        {
            Parameters = new List<GameObject>();
        }
        
        private void Awake()
        {
            Selectable.OnSelect += Inspect;

            OriginSize = _body.sizeDelta;
            
            Clear();
        }

        private void Inspect(Selectable selectable)
        {
            Clear();

            if (!selectable.Inspect)
            {
                return;
            }
            
            _inspector.SetActive(true);

            _inspectName.text = selectable.Name;

            _description.text = selectable.Description;

            if (selectable is IInspect inspect)
            {
                SetupParameters(inspect.Parameters);
            }
        }

        private void SetupParameters(object behavior)
        {
            var offset = 0;
            
            foreach (var property in behavior.GetType().GetProperties())
            {
                var attribute = property.GetCustomAttribute<ParameterAttribute>();
                
                if (attribute == null) continue;

                var type = property.PropertyType;

                GameObject instance = default;
                
                if (type == typeof(float))
                {
                    instance = Instantiate(_floatParameter, _body);
                }
                else if (type == typeof(int))
                {
                    instance = Instantiate(_intParameter, _body);
                }
                else if (type == typeof(uint))
                {
                    instance = Instantiate(_uintParameter, _body);
                }
                else if (type == typeof(bool))
                {
                    instance = Instantiate(_booleanParameter, _body);
                }
                else if (type == typeof(Vector3))
                {
                    instance = Instantiate(_vectorParameter, _body);
                }
                
                if (instance == default) continue;

                var propertyInstance = instance.GetComponent<InspectEmpty>();

                propertyInstance.SetupParameter(property, behavior);

                var rect = instance.GetComponent<RectTransform>();

                rect.localPosition -= new Vector3(0, offset);

                var sizeDelta = rect.sizeDelta;
                
                offset += (int) sizeDelta.y + 25;
                
                _body.sizeDelta += Vector2.up * (sizeDelta.y + 25);

                Parameters.Add(instance);
            }
        }

        private void Clear()
        {
            _body.sizeDelta = OriginSize;
            
            foreach (var parameter in Parameters)
            {
                Destroy(parameter);
            }
            
            _inspector.SetActive(false);
        }
    }
}