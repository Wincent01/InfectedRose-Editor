using UnityEngine;

namespace Tree
{
    public class ConnectionLine : Selectable
    {
        public Connection ConnectionA { get; set; }

        public Connection ConnectionB { get; set; }

        public float Width { get; set; }

        public RectTransform Rect { get; private set; }

        public static ConnectionLine Current { get; set; }

        private void Awake()
        {
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
            return GetOther(connection.Entry);
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