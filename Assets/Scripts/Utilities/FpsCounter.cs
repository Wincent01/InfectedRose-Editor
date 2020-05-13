using System;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private float DeltaTime { get; set; }

        private void Update()
        {
            DeltaTime += (Time.unscaledDeltaTime - DeltaTime) * 0.1f;

            var ms = DeltaTime * 1000.0f;
            var fps = 1.0f / DeltaTime;
            
            _text.text = $"{ms:0.0} ms ({fps:0.} fps)";
        }
    }
}