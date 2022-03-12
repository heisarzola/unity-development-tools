

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Delete Empty Folders Window.
 *       
 *   <<< DESCRIPTION >>>
 *       -- A dockable editor window in charge of finding and deleting all empty folders (and their .meta) from the project. Useful when using Git repositories.
 *
 *   <<< LIMITATIONS >>>
 *       -- It doesn't discriminate folders when deleting.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: DeleteEmptyFolders
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class DeleteEmptyFoldersWindow : EditorWindow
{
    private List<DirectoryInfo> _emptyDirs;
    private bool _lastCleanOnSave;

    bool HasNoEmptyDir { get { return _emptyDirs == null || _emptyDirs.Count == 0; } }

    [MenuItem("Tools/Project/Delete Empty Folders")]
    public static void ShowWindow()
    {
        var w = GetWindow<DeleteEmptyFoldersWindow>();
        w.titleContent = new GUIContent("Delete Empty Folders");
    }

    void OnEnable()
    {
        _lastCleanOnSave = DeleteEmptyFolders.CleanOnSave;

        minSize = new Vector2(300, 44);
    }

    void OnDisable()
    {
        DeleteEmptyFolders.CleanOnSave = _lastCleanOnSave;
    }

    void OnGUI()
    {

        EditorGUILayout.BeginVertical();
        {
            {
                if (GUILayout.Button("Find and Delete Empty Folders"))
                {
                    DeleteEmptyFolders.FillEmptyDirList(out _emptyDirs);

                    if (HasNoEmptyDir)
                    {
                        Debug.Log("No empty folders were found.");
                    }
                    else
                    {
                        DeleteEmptyFolders.DeleteAllEmptyDirAndMeta(ref _emptyDirs);
                    }
                }
            }

            bool cleanOnSave = GUILayout.Toggle(_lastCleanOnSave, new GUIContent("Clean Empty Folders Automatically On Save",
                "This option requires this tab to be open somewhere, so please dock it wherever you like."));

            if (cleanOnSave != _lastCleanOnSave)
            {
                _lastCleanOnSave = cleanOnSave;
                DeleteEmptyFolders.CleanOnSave = cleanOnSave;
            }
        }

        EditorGUILayout.EndVertical();
    }
}