using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Behaviors;
using Inspect;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Tree
{
    public class Node : Selectable, IInspect
    {
        [SerializeField] private GameObject _connectionPrefab;

        [SerializeField] private Image _image;

        public GameObject ConnectionPrefab => _connectionPrefab;

        private ConnectionEntry[] _connections;

        public Connection Entry { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public NodeConnection[] Connections { get; private set; }
        
        public BehaviorBase Behavior { get; set; }

        public object Parameters => Behavior;

        public void StartConnections()
        {
            SetupConnections();
            
            _connections = _connections.Where(c => c.Enabled).ToArray();
            
            Connections = new NodeConnection[_connections.Length];

            for (var index = 0; index < _connections.Length; index++)
            {
                var connection = _connections[index];

                var info = transform;

                var instance = Instantiate(
                    _connectionPrefab,
                    info.position,
                    Quaternion.identity, info
                );

                instance.transform.localPosition += (Vector3) connection.Offset;

                var connect = instance.GetComponent<Connection>();

                connect.Node = this;

                connect.Info = connection;
                
                if (connection.Entry)
                {
                    Entry = connect;

                    connect.Entry = true;
                }

                var rect = instance.GetComponent<RectTransform>();

                rect.Anchor(connection.Side);
                
                Connections[index] = new NodeConnection
                {
                    Entry = connection,
                    Instance = instance,
                    Transform = rect,
                    Connection = connect
                };
            }
        }

        private void SetupConnections()
        {
            var connections = new List<ConnectionEntry>();
            
            if (Behavior == default)
            {
                _connections = connections.ToArray();
                
                return;
            }

            if (!(Behavior is RootBehavior))
            {
                connections.Add(new ConnectionEntry
                {
                    Entry = true,
                    Name = "Entry node",
                    Description = "Connect conditions to this point for them to flow into this node",
                    Enabled = true,
                    Side = ConnectionSide.Left
                });
            }

            var points = new (ConnectionSide side, Vector2 offset)[]
            {
                (ConnectionSide.Right, Vector2.zero),
                (ConnectionSide.Right, Vector2.up * 70),
                (ConnectionSide.Right, Vector2.down * 70),
                (ConnectionSide.Upper, Vector2.zero),
                (ConnectionSide.Upper, Vector2.right * 70),
                (ConnectionSide.Upper, Vector2.left * 70),
                (ConnectionSide.Bottom, Vector2.zero),
                (ConnectionSide.Bottom, Vector2.up * 70),
                (ConnectionSide.Bottom, Vector2.down * 70),
            };

            var index = 0;
            
            foreach (var property in Behavior.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(BehaviorBase)) continue;
                
                var attribute = property.GetCustomAttribute<ParameterAttribute>();

                if (attribute == null) continue;
                
                var (side, offset) = points[index++];
                
                connections.Add(new ConnectionEntry
                {
                    Enabled = true,
                    Name = attribute.Display,
                    Description = attribute.Name,
                    Side = side,
                    Offset = offset,
                    Property = property
                });
            }

            _connections = connections.ToArray();
        }

        private void Update()
        {
            if (Held)
            {
                transform.position = Input.mousePosition - MouseOffset;
            }

            if (IsSelected)
            {
                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Export(List<Node> nodes)
        {
            if (nodes.Contains(this)) return;

            nodes.Add(this);
            
            var behavior = Behavior;
            
            foreach (var connection in Connections)
            {
                if (connection.Entry.Entry) continue;
                
                var others = connection.Connection.Others;

                var behaviors = new List<BehaviorBase>();

                foreach (var line in connection.Connection.Lines)
                {
                    var current = line.GetOther(this).Node.Behavior;

                    var extra = line.Extra;

                    if (Math.Abs(extra.Duration) > 0.01f)
                    {
                        current = new DurationBehavior
                        {
                            Action = current,
                            Duration = extra.Duration
                        };
                    }
                    
                    if (Math.Abs(extra.Delay) > 0.01f || extra.Intervals != 0)
                    {
                        current = new AttackDelayBehavior
                        {
                            Action = current,
                            Delay = extra.Delay,
                            Intervals = extra.Intervals,
                            IgnoreInterrupts = extra.IgnoreInterrupts
                        };
                    }

                    behaviors.Add(current);
                }
                
                if (behaviors.Count == default) continue;

                if (behaviors.Count > 1)
                {
                    var and = new AndBehavior
                    {
                        Branches = behaviors.ToList()
                    };

                    connection.Entry.Property.SetValue(behavior, and);
                }
                else
                {
                    connection.Entry.Property.SetValue(behavior, behaviors[0]);
                }

                foreach (var other in others)
                {
                    other.Export(nodes);
                }
            }
        }
        
        public void UpdateIcon(Sprite sprite)
        {
            Icon = sprite;

            _image.sprite = Icon;
        }

        private void OnDestroy()
        {
            foreach (var connection in Connections.Select(c => c.Instance))
            {
                Destroy(connection);
            }
        }
    }
}
