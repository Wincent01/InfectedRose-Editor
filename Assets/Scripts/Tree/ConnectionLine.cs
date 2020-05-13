using System;
using Behaviors;
using Inspect;
using Tree.Concepts;
using UnityEngine;
using UnityEngine.UI;

namespace Tree
{
    public class ConnectionLine : Selectable, IInspect
    {
        [SerializeField] private Image _image;
        
        public Connection ConnectionA { get; set; }

        public Connection ConnectionB { get; set; }

        public float Width { get; set; }

        public RectTransform Rect { get; private set; }
        
        public LineParameters Extra { get; private set; }

        public static ConnectionLine Current { get; set; }

        public object Parameters => Extra;

        private void Awake()
        {
            Extra = new LineParameters();
            
            Rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector3 b;

            if (ConnectionB != null)
            {
                b = ConnectionB.transform.position;
            }
            else
            {
                b = Input.mousePosition;
            }

            var color = Color.green;

            if (Math.Abs(Extra.Delay) > 0.01f || Extra.Intervals != 0)
            {
                color = new Color(1f, 0.47f, 0.15f);
            }
            else if (Math.Abs(Extra.Duration) > 0.01f)
            {
                color = Color.blue;
            }

            _image.color = color;

            Map(ConnectionA.transform.position, b);

            CheckActions();
        }

        private void Map(Vector3 a, Vector3 b)
        {
            Rect.position = a;
            
            a = TreeManager.Workspace.InverseTransformPoint(a);

            b = TreeManager.Workspace.InverseTransformPoint(b);

            var differenceVector = b - a;

            TreeManager.Workspace.TransformPoint(differenceVector);

            Rect.sizeDelta = new Vector2(differenceVector.magnitude, Width);

            Rect.pivot = new Vector2(0, 0.5f);

            var angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;

            Rect.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void CheckActions()
        {
            if (Selected != this) return;

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Destroy(gameObject);
            }
        }

        public void SetupConnections()
        {
            ConnectionB.Lines.Add(this);
            ConnectionA.Lines.Add(this);
        }

        public bool IsLinked(Node connection)
        {
            return IsLinked(connection.Entry);
        }

        public bool IsLinked(Connection connection)
        {
            return ConnectionA == connection || ConnectionB == connection;
        }

        public Connection GetOther(Node connection)
        {
            return ConnectionA.Node == connection ? ConnectionB : ConnectionA;
        }

        public Connection GetOther(Connection connection)
        {
            return connection == ConnectionA ? ConnectionB : ConnectionA;
        }

        private void OnDestroy()
        {
            if (ConnectionB != default)
                ConnectionB.Lines.Remove(this);

            if (ConnectionA != default)
                ConnectionA.Lines.Remove(this);
        }
    }
}