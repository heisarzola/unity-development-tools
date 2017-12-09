
/*---------------- Creation Date: 18-Feb-17 -----------------//
//------------ Last Modification Date: 18-Feb-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Quick Folder Opener
 *       
 *   <<< DESCRIPTION >>>
 *       -- Provides several shorcuts for quickly opening several common-use folders.
 *
 *   <<< LIMITATIONS >>>
 *       -- Asset store folder name is hardcoded in the constant "ASSET_STORE_FOLDER_NAME" and needs to be replaced on major updates. (Folder might be renamed in Unity 2017, and so on soon, so adjustments probably need to be made then).
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: None.
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
*   <<< TO-DO LIST >>>
*       -- <<< EMPTY >>>
*
*   <<< POSSIBLES >>>
*       -- <<< EMPTY >>>
*
*   <<< SOURCES >>>
*       -- [1] Base : https://www.assetstore.unity3d.com/en/#!/content/68761
*       -- [2] Asset Store Packagues Locations : http://answers.unity3d.com/questions/45050/where-unity-store-saves-the-packages.html
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Feb-17 >>>
 *       -- Class creation, removed many other components from a free asset. [1]
//----------------------------------------------------------------------*/

using UnityEditor;
using System.IO;
using UnityEngine;

public class QuickOpener // [1]
{
    private const string _ASSET_STORE_FOLDER_NAME = "Asset Store-5.x";

    [MenuItem("Tools/Quick Opener/Application.dataPath", false, 100)]
    private static void OpenDataPath()
    {
        Reveal(Application.dataPath);
    }

    [MenuItem("Tools/Quick Opener/Application.persistentDataPath", false, 100)]
    private static void OpenPersistentDataPath()
    {
        Reveal(Application.persistentDataPath);
    }

    [MenuItem("Tools/Quick Opener/Application.streamingAssetsPath", false, 100)]
    private static void OpenStreamingAssets()
    {
        Reveal(Application.streamingAssetsPath);
    }

    [MenuItem("Tools/Quick Opener/Application.temporaryCachePath", false, 100)]
    private static void OpenCachePath()
    {
        Reveal(Application.temporaryCachePath);
    }

    [MenuItem("Tools/Quick Opener/Asset Store Packages Folder", false, 111)] // [2]
    private static void OpenAssetStorePackagesFolder()
    {
#if UNITY_EDITOR_OSX
            string path = GetAssetStorePackagesPathOnMac();
#elif UNITY_EDITOR_WIN
            string path = GetAssetStorePackagesPathOnWindows();
#endif

        Reveal(path);
    }

    [MenuItem("Tools/Quick Opener/Editor Application Path")]
    private static void OpenUnityEditorPath()
    {
        Reveal(new FileInfo(EditorApplication.applicationPath).Directory.FullName);
    }

    [MenuItem("Tools/Quick Opener/Editor Log Folder")]
    private static void OpenEditorLogFolderPath()
    {
#if UNITY_EDITOR_OSX
			string rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			string libraryPath = Path.Combine(rootFolderPath, "Library");
			string logsFolder = Path.Combine(libraryPath, "Logs"); 
			string UnityFolder = Path.Combine(logsFolder, "Unity");
			Reveal(UnityFolder);
#elif UNITY_EDITOR_WIN
            string rootFolderPath = System.Environment.ExpandEnvironmentVariables("%localappdata%");
            string unityFolder = Path.Combine(rootFolderPath, "Unity");
            Reveal(Path.Combine(unityFolder, "Editor"));
#endif
    }

    private static string GetAssetStorePackagesPathOnMac()
    {
        string rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        string libraryPath = Path.Combine(rootFolderPath, "Library");
        string unityFolder = Path.Combine(libraryPath, "Unity");
        return Path.Combine(unityFolder, _ASSET_STORE_FOLDER_NAME);
    }

    private static string GetAssetStorePackagesPathOnWindows()
    {
        string rootFolderPath = System.Environment.ExpandEnvironmentVariables("%appdata%");
        string unityFolder = Path.Combine(rootFolderPath, "Unity");
        return Path.Combine(unityFolder, _ASSET_STORE_FOLDER_NAME);
    }

    public static void Reveal(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Debug.LogWarning(string.Format("Folder '{0}' does not exist.", folderPath));
            return;
        }

        EditorUtility.RevealInFinder(folderPath);
    }
}