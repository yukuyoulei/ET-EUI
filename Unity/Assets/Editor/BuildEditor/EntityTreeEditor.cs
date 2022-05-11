using UnityEditor;
using UnityEngine;
public class EntityTreeEditor : EditorWindow
{
    [MenuItem("Tools/EntityTree")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EntityTreeEditor));
    }
    private void OnGUI()
    {
        
    }
}