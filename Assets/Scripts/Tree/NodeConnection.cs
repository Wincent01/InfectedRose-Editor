using UnityEngine;

namespace Tree
{
    public class NodeConnection
    {
        public ConnectionEntry Entry { get; set; }
        
        public GameObject Instance { get; set; }
        
        public RectTransform Transform { get; set; }
        
        public Connection Connection { get; set; }
    }
}