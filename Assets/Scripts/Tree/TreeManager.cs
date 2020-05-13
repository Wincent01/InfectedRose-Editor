using System;
using UnityEngine;

namespace Tree
{
    public class TreeManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _workspace;

        [SerializeField] private float _speed;

        public static TreeManager Singleton { get; private set; }
        
        public static Vector3 Origin { get; private set; }

        public static RectTransform Workspace => Singleton._workspace;

        public static float Speed => Singleton._speed;
        
        private void Awake()
        {
            Origin = _workspace.transform.position;
            
            Singleton = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Workspace.transform.position = Origin;
                
                return;
            }

            var scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Math.Abs(scroll) > 0.01f)
            {
                var scale = _workspace.localScale;

                var mag = scale.magnitude + scroll;

                mag = Mathf.Clamp(mag, 0.2f, 5);

                _workspace.localScale = scale.normalized * mag;

                _workspace.localPosition /= scale.magnitude / _workspace.localScale.magnitude;
            }
            
            var delta = Vector2.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                delta += Vector2.up;
            }
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                delta += Vector2.down;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                delta += Vector2.left;
            }
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                delta += Vector2.right;
            }
            
            if (delta == Vector2.zero) return;
            
            _workspace.Translate(-delta * (_speed * Time.deltaTime));
        }
    }
}