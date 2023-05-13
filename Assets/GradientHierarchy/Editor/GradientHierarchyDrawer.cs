using UnityEditor;
using UnityEngine;

namespace GradientHierarchy.Editor
{
  [InitializeOnLoad]
  public class GradientHierarchyDrawer
  {
    static GradientHierarchyDrawer()
    {
      EditorApplication.hierarchyWindowItemOnGUI +=
        HierarchyWindowItemOnGUIHandler;
      
      EditorApplication.update += Update;
    }

    private static void Update()
    {
      EditorApplication.RepaintHierarchyWindow();
    }

    private static void HierarchyWindowItemOnGUIHandler(int instanceId, Rect selectionRect)
    {
      var currentObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
      if (!currentObject)
        return;
      
      var strength = Mathf.Sin((float)EditorApplication.timeSinceStartup) * 0.04f;
      
      var parentComponents = currentObject.transform.GetComponentsInParent<Transform>();
      var parentCount = parentComponents.Length - 1;
      var alpha = Mathf.Clamp01(0.2f - parentCount  *strength);

      var backgroundColor = new Color(0.75f, 0.57f, 0.99f, alpha);
      EditorGUI.DrawRect(selectionRect, backgroundColor);
    }
  }
}