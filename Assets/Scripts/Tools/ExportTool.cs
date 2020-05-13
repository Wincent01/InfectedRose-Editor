using System.Collections.Generic;
using Behaviors;
using Behaviors.External;
using Tree;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools
{
    public class ExportTool : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var node in FindObjectsOfType<Node>())
            {
                if (!(node.Behavior is RootBehavior root)) continue;

                node.Export(new List<Node>());
                
                var skill = new Skill
                {
                    Cost = root.Cost,
                    Cooldown = root.Cooldown,
                    CooldownGroup = root.CooldownGroup,
                    Icon = root.Icon,
                    Root = root.Next
                };

                XmlExporter.Export("./export.xml", skill);
                
                break;
            }
        }
    }
}