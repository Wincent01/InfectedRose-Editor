using System;
using System.Linq;
using System.Reflection;
using Behaviors;
using Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Selectable = Tree.Selectable;

namespace Creator
{
    public class CreatorNode : Selectable
    {
        [SerializeField] private Template _template;

        [SerializeField] private Image _sprite;

        [SerializeField] private GameObject _nodePrefab;

        private Vector2 Origin { get; set; }

        private void Update()
        {
            if (Held)
            {
                transform.position = Input.mousePosition - MouseOffset;
            }
        }

        protected override void Down(PointerEventData eventData)
        {
            var info = transform;

            Instantiate(gameObject, info.position, info.rotation, info.parent);

            Origin = info.position;
        }

        protected override void Up(PointerEventData eventData)
        {
            if (Vector2.Distance(Origin, transform.position) < 20)
            {
                Destroy(gameObject);

                return;
            }

            var baseType = typeof(BehaviorBase);

            var types = baseType.Assembly.GetTypes().Where(t => t.BaseType == baseType);

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<BehaviorAttribute>();

                if (attribute == null) continue;

                if (attribute.Template != _template) continue;

                var info = transform;

                var instance = Instantiate(_nodePrefab, info.position, info.rotation, TreeManager.Workspace.transform);

                instance.name = gameObject.name;
                
                var node = instance.GetComponent<Node>();

                node.Name = Name;

                node.Description = Description;

                node.Behavior = Activator.CreateInstance(type) as BehaviorBase;

                node.UpdateIcon(_sprite.sprite);
                
                node.StartConnections();

                Destroy(gameObject);

                return;
            }
            
            Destroy(gameObject);
        }
    }
}