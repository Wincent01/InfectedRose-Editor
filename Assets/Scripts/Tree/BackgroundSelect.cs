using UnityEngine;
using UnityEngine.EventSystems;

namespace Tree
{
    public class BackgroundSelect : Selectable
    {
        private Vector3 OldPosition { get; set; }
        
        private void Update()
        {
            if (!Held) return;

            if (OldPosition == Vector3.zero)
            {
                OldPosition = TreeManager.Origin;
            }

            var delta = Input.mousePosition - OldPosition;

            OldPosition = Input.mousePosition;

            TreeManager.Workspace.Translate(delta * (250 * Time.deltaTime));
        }

        protected override void Down(PointerEventData eventData)
        {
            OldPosition = Input.mousePosition;
        }

        protected override void Up(PointerEventData eventData)
        {
            OldPosition = Vector3.zero;
        }
    }
}