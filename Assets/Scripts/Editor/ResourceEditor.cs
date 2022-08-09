using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace Editor
{
    public sealed class ResourceUnload
    {
        [MenuItem("Assets/Unload Assets")] 
        static void UnloadAssets()
        {
            Resources.UnloadUnusedAssets();
        }
    
    }
}
 
#endif