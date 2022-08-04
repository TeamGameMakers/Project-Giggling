using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Editor
{
    public class EditorResource
    {
        [MenuItem("Assets/Unload Assets")]
        public static void UnloadAssets()
        {
            Resources.UnloadUnusedAssets();
        }

    }
}
 
#endif