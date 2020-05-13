#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Utilities
{
    public class GameBuilder
    {
        private static string[] ReadNames()
        {
            var temp = new List<string>();

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled) continue;

                /*
                var name = S.path.Substring(S.path.LastIndexOf('/')+1);
                
                name = name.Substring(0,name.Length-6);
                */

                temp.Add(scene.path);
            }

            return temp.ToArray();
        }

        [MenuItem("Tools/Build/Linux")]
        public static void BuildLinux()
        {
            var dialog = EditorUtility.OpenFolderPanel("Output", "", "");

            BuildPipeline.BuildPlayer(ReadNames(), Path.Combine(dialog, "InfectedRose - Editor"), BuildTarget.StandaloneLinux64, BuildOptions.None);
        }
    }
}

#endif