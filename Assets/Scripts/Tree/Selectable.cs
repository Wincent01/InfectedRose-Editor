using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Tree
{
    public abstract class Selectable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [FormerlySerializedAs("_name")] [SerializeField] private string _selectableName;

        [SerializeField] private string _description;

        [SerializeField] private bool _inspect;

        public string Name
        {
            get => _selectableName;
            set => _selectableName = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public bool Inspect => _inspect;

        public static Selectable Selected { get; private set; }
        
        public static event Action<Selectable> OnSelect;
        
        public bool Held { get; private set; }

        public bool IsSelected => Selected == this;
        
        protected Vector3 MouseOffset { get; private set; }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Selected = this;

            MouseOffset = Input.mousePosition - transform.position;

            Held = true;

            OnSelect?.Invoke(this);

            Down(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Held = false;
            
            Up(eventData);
        }

        protected virtual void Down(PointerEventData eventData)
        {
        }

        protected virtual void Up(PointerEventData eventData)
        {
        }
    }
}