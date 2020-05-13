using UnityEngine;

namespace Utilities
{
    public class Hotkeys : MonoBehaviour
    {
        private bool FullScreen { get; set; }
        
        private int WindowedWidth { get; set; }
        
        private int WindowedHeight { get; set; }

        private void Awake()
        {
            FullScreen = Screen.fullScreen;

            if (FullScreen)
            {
                UpdateWindowedDetails();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                FullScreen = !FullScreen;
                
                if (FullScreen)
                {
                    UpdateWindowedDetails();
                    
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                }
                else
                {
                    Screen.SetResolution(WindowedWidth, WindowedHeight, false);
                }
            }
        }

        private void UpdateWindowedDetails()
        {
            WindowedWidth = Screen.width;

            WindowedHeight = Screen.height;
        }
    }
}