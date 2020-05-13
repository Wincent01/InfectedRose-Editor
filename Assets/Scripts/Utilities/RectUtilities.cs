using System;
using Tree;
using UnityEngine;

namespace Utilities
{
    public static class RectUtilities
    {
        public static void Anchor(this RectTransform @this, ConnectionSide side)
        {
            Vector2 anchor;
            
            switch (side)
            {
                case ConnectionSide.Upper:
                    anchor = new Vector2 {x = 0.5f, y = 1f};
                    break;
                case ConnectionSide.Left:
                    anchor = new Vector2 {x = 0, y = 0.5f};
                    break;
                case ConnectionSide.Right:
                    anchor = new Vector2 {x = 1, y = 0.5f};
                    break;
                case ConnectionSide.Bottom:
                    anchor = new Vector2 {x = 0.5f, y = 0};
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }

            @this.anchorMin = anchor;
            @this.anchorMax = anchor;
            @this.pivot = anchor;
        }
    }
}