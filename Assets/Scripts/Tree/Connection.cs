using System.Collections.Generic;
using System.Linq;
using Behaviors;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tree
{
    public class Connection : Selectable
    {
        [SerializeField] private GameObject _linePrefab;

        [SerializeField] private float _lineWidth;

        [FormerlySerializedAs("_name")] [SerializeField] private TextMeshProUGUI _connectionName;

        public GameObject LinePrefab => _linePrefab;

        public Image Image { get; private set; }
        
        public Node Node { get; set; }
        
        public bool Entry { get; set; }
        
        public ConnectionEntry Info { get; set; }
        
        public List<ConnectionLine> Lines { get; }

        public Node[] Others
        {
            get
            {
                var others = new List<Node>();

                foreach (var line in Lines)
                {
                    var other = line.GetOther(this);

                    others.Add(other.Node);
                }

                return others.ToArray();
            }
        }

        public Connection()
        {
            Lines = new List<ConnectionLine>();
        }

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        private void Start()
        {
            Name = Info.Name;

            Description = Info.Description;
            
            if (Entry)
            {
                Image.color = Color.green;
            }
            else
            {
                Image.color = Color.gray;
            }
        }

        private void Update()
        {
            if (Entry || Info == null) return;

            _connectionName.text = Info.Name;
            
            if (Lines.Count != default)
            {
                Image.color = Color.yellow;
            }
            else
            {
                Image.color = Color.gray;
            }
        }

        protected override void Down(PointerEventData eventData)
        {
            if (Entry) return;

            var instance = Instantiate(_linePrefab, TreeManager.Workspace);

            var line = instance.GetComponent<ConnectionLine>();

            line.Width = _lineWidth;

            line.ConnectionA = this;
            
            ConnectionLine.Current = line;
        }

        protected override void Up(PointerEventData eventData)
        {
            if (Entry) return;

            var nodes = FindObjectsOfType<Node>().ToList();

            nodes.Remove(Node);

            var position = Input.mousePosition;

            nodes.Sort((a, b) => (int)
                (Vector3.Distance(a.transform.position, position) -
                 Vector3.Distance(b.transform.position, position))
            );

            var connect = nodes.FirstOrDefault();

            if (connect != default)
            {
                if (connect.Behavior is RootBehavior)
                {
                    connect = default;
                }
                else if (Lines.Any(l => l.IsLinked(connect)))
                {
                    connect = default;
                }
            }
            
            if (connect == default || Vector3.Distance(connect.transform.position, position) > 200)
            {
                Destroy(ConnectionLine.Current.gameObject);

                return;
            }
            
            if (Selected == default) return;

            ConnectionLine.Current.ConnectionB = connect.Entry;
            
            ConnectionLine.Current.SetupConnections();

            ConnectionLine.Current = default;
        }

        private void OnDestroy()
        {
            foreach (var line in Lines.ToArray())
            {
                Destroy(line.gameObject);
            }
        }
    }
}
