

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Group Utility.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Utility in charge of grouping the selected scene objects.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module:
 *          -- GameObject
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GroupUtility : Editor
{
    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [MenuItem("Edit/Group %g", false)]
    public static void Group()
    {
        if (Selection.transforms.Length > 0)
        {
            GameObject group = new GameObject("Group");

            // set pivot to average of selection
            Vector3 pivotPosition = Vector3.zero;
            foreach (Transform g in Selection.transforms)
            {
                pivotPosition += g.transform.position;
            }
            pivotPosition /= Selection.transforms.Length;
            group.transform.position = pivotPosition;

            // register undo as we parent objects into the group
            Undo.RegisterCreatedObjectUndo(group, "Group");
            foreach (GameObject s in Selection.gameObjects)
            {
                Undo.SetTransformParent(s.transform, group.transform, "Group");
            }

            Selection.activeGameObject = group;
        }
        else
        {
            Debug.LogWarning("You must select one or more objects.");
        }
    }
}