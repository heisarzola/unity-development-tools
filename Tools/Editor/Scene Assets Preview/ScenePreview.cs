
/*---------------- Creation Date: 06-Dec-17 -----------------//
//------------ Last Modification Date: 09-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Scene Preview.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Editor class intended to show a preview of a given scene file when seen in the inspector. It gets updated every time a scene is played.
 *
 *   <<< LIMITATIONS >>>
 *       -- Will make a folder within the Assets/Widgets/ directory, however the screenshots will never be present in a build.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None
 *       -- Module: None
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Almost entire class : https://diegogiacomelli.com.br/unity3d-scenepreview-inspector/
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 06-Dec-17 >>>
 *       -- Created base class and adapted source [1] to work without necessarily having to manually create folders, works in a folder that is ignored by compiler, and has neatly organized instructions when preview is unavailable.
 *   <<< V.1.0.1 -- 09-Dec-17 >>>
 *       -- If a thumbnail already exists, a new one won't be created.
 //----------------------------------------------------------------------*/

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;

[CustomEditor(typeof(SceneAsset))]
[CanEditMultipleObjects]
public class ScenePreview : Editor
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private const string _PREVIEW_FOLDERS = "Scene Preview Thumbnails";
    private const float _EDITOR_MARGIN = 50;
    private const float _PREVIEW_MARGIN = 5;

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Quick method to capture a screenshot the moment the editor is in runtime.
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    public static void CaptureScreenshot()
    {
        var previewPath = GetPreviewPath(SceneManager.GetActiveScene().name);
        ScreenCapture.CaptureScreenshot(previewPath);
    }

    /// <summary>
    /// Override default inspector and draw a custom one for scene assets.
    /// </summary>
    public override void OnInspectorGUI()
    {
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

        var sceneNames = targets.Select(t => ((SceneAsset)t).name).OrderBy(n => n).ToArray();
        var previewsCount = sceneNames.Length;
        var previewWidth = Screen.width;
        var previewHeight = (Screen.height - _EDITOR_MARGIN * 2 - (_PREVIEW_MARGIN * previewsCount)) / previewsCount;

        for (int i = 0; i < sceneNames.Length; i++)
        {
            DrawPreview(i, sceneNames[i], previewWidth, previewHeight);
        }
    }

    /// <summary>
    /// Draw a texture in the inspector based on the scene name given.
    /// </summary>
    private void DrawPreview(int index, string sceneName, float width, float height)
    {
        string previewPath = GetPreviewPath(sceneName);


        if (!File.Exists(previewPath))
        {
            EditorGUILayout.TextArea(string.Format(
                "There is no image preview for the scene:\n\"{0}\".\n\nThat is located on the the path:\n\"{1}\".\n\nTo create a preview, just load and run this scene in the editor. A thumbnail will be created on the first frame of runtime.",
                sceneName.AddRichTextTag_Color(EditorGUIUtility.isProSkin ? RichTextColors.yellow : RichTextColors.red),
                previewPath.AddRichTextTag_Color(EditorGUIUtility.isProSkin ? RichTextColors.yellow : RichTextColors.red))
                , new GUIStyle("HelpBox") { richText = true });
        }
        else
        {
            GUI.DrawTexture(new Rect(index, _EDITOR_MARGIN + index * (height + _PREVIEW_MARGIN), width, height),
                Resources.Load(sceneName) as Texture, ScaleMode.ScaleToFit);
        }
    }

    /// <summary>
    /// Gets a path to store/retrieve screenshots that will be ignored while making a build, but not while on editor.
    /// </summary>
    /// <param name="sceneName">Scene name without extension.</param>
    private static string GetPreviewPath(string sceneName)
    {
        string folderPath = Application.dataPath.Append("/Gizmos/", _PREVIEW_FOLDERS, "/Editor/Resources");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return string.Format("{0}/{1}.png", folderPath, sceneName);
    }

}//End of class