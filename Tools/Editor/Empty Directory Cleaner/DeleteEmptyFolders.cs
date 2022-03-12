

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Delete Empty Folders.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class behind the directory cleaning logic used in the window of the same name.
 *
 *   <<< LIMITATIONS >>>
 *       -- It doesn't discriminate folders when deleting.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module: DeleteEmptyFoldersWindow
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

public class DeleteEmptyFolders : UnityEditor.AssetModificationProcessor
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private const string CLEAN_ON_SAVE_KEY = "AUTO_DELETE_EMPTY_FOLDERS_ON_SAVE";

    // return: Is this directory empty?
    delegate bool IsEmptyDirectory(DirectoryInfo dirInfo, bool areSubDirsEmpty);

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    public static bool CleanOnSave
    {
        get
        {
            return EditorPrefs.GetBool(CLEAN_ON_SAVE_KEY, false);
        }
        set
        {
            EditorPrefs.SetBool(CLEAN_ON_SAVE_KEY, value);
        }
    }
    
    public static void DeleteAllEmptyDirAndMeta(ref List<DirectoryInfo> emptyDirs)  
    {
        StringBuilder sb = new StringBuilder();
        foreach (var dirInfo in emptyDirs)
        {
            sb.Length = 0;
            sb.Capacity = 0;
            sb.Append("Found and deleted the empty folder: \"");
            sb.Append(dirInfo.FullName);
            sb.Append("\"");
            AssetDatabase.MoveAssetToTrash(GetRelativePathFromCd(dirInfo.FullName));
            Debug.Log(sb.ToString());
        }

        emptyDirs.Clear();
    }

    public static void FillEmptyDirList(out List<DirectoryInfo> emptyDirs)
    {
        var newEmptyDirs = new List<DirectoryInfo>();
        emptyDirs = newEmptyDirs;

        var assetDir = new DirectoryInfo(Application.dataPath);

        WalkDirectoryTree(assetDir, (dirInfo, areSubDirsEmpty) =>
        {
            bool isDirEmpty = areSubDirsEmpty && DirHasNoFile(dirInfo);
            if (isDirEmpty)
                newEmptyDirs.Add(dirInfo);
            return isDirEmpty;
        });
    }

    public static string GetRelativePath(string filespec, string folder)
    {
        Uri pathUri = new Uri(filespec);
        // Folders must end in a slash
        if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            folder += Path.DirectorySeparatorChar;
        }
        Uri folderUri = new Uri(folder);
        return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
    }

    private static bool WalkDirectoryTree(DirectoryInfo root, IsEmptyDirectory pred)
    {
        DirectoryInfo[] subDirs = root.GetDirectories();

        bool areSubDirsEmpty = true;
        foreach (DirectoryInfo dirInfo in subDirs)
        {
            if (false == WalkDirectoryTree(dirInfo, pred))
                areSubDirsEmpty = false;
        }

        bool isRootEmpty = pred(root, areSubDirsEmpty);
        return isRootEmpty;
    }

    private static bool DirHasNoFile(DirectoryInfo dirInfo)
    {
        FileInfo[] files = null;

        try
        {
            files = dirInfo.GetFiles("*.*");
            files = files.Where(x => !IsMetaFile(x.Name)).ToArray();
        }
        catch (Exception)
        {
        }

        return files == null || files.Length == 0;
    }

    private static string GetRelativePathFromCd(string filespec)
    {
        return GetRelativePath(filespec, Directory.GetCurrentDirectory());
    }

    private static bool IsMetaFile(string path)
    {
        return path.EndsWith(".meta");
    }
}