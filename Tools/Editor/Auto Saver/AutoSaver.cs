

/*---------------- Creation Date: 15-Dec-17 -----------------//
//------------ Last Modification Date: 19-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Auto Saver.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class in charge of autosaving scenes after a set interval of time, before entering playmode and in other relevant moments.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
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
 *       -- [1] Base class, saves every 5 minutes : https://github.com/liortal53/AutoSaveScene/blob/master/Assets/Editor/AutoSaveScene.cs
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 15-Dec-17 >>>
 *       -- Class created, supports auto save every 5 minutes, when playmode starts, it also attempts to save before editor closes.
 *   <<< V.1.0.1 -- 19-Dec-17 >>>
 *       -- Auto save settings should now be visible within the Preferences tab.
 *       -- Added options to personalize auto save time, save before runtime, save backups, max amount of backups, and custom backup folder.
 *       -- Implemented backup scene saving [1], improving it by allowing a max amount of scene backup per original scene.
//----------------------------------------------------------------------*/

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Sakura.Core.Tools
{

    [InitializeOnLoad]
    public class AutoSaver
    {
        //------------------------------------------------------------------------------------//
        //----------------------------------- FIELDS -----------------------------------------//
        //------------------------------------------------------------------------------------//

        private const string _AUTO_SAVER_TIMER = "_AUTO_SAVER_TIMER";
        private const string _AUTO_SAVER_ON = "_AUTO_SAVER_ON";
        private const string _AUTO_SAVER_ON_WHILE_SAVING_BACKUP_AS_WELL = "_AUTO_SAVER_ON_WHILE_SAVING_BACKUP_AS_WELL";
        private const string _AUTO_SAVER_SAVE_ON_EDITOR_FOLDER_FOLDER = "_AUTO_SAVER_SAVE_ON_EDITOR_FOLDER_FOLDER";
        private const string _AUTO_SAVER_SHOULD_SAVE_ON_EDITOR_FOLDER = "_AUTO_SAVER_SHOULD_SAVE_ON_EDITOR_FOLDER";
        private const string _AUTO_SAVER_SAVE_BEFORE_RUNTIME = "_AUTO_SAVER_SAVE_BEFORE_RUNTIME";
        private const string _AUTO_SAVER_MAX_BACKUP_AMOUNT = "_AUTO_SAVER_MAX_BACKUP_AMOUNT";
        private const string _DATE_TIME_STRING_FORMAT = "yyyy-MM-dd_HH-mm-ss";

        private static System.DateTime _lastSaveTime = System.DateTime.Now;
        private static System.TimeSpan _updateInterval;

        private static bool _isGameRunning;

        private static GUIStyle centeredLabelStyle;

        //------------------------------------------------------------------------------------//
        //--------------------------------- PROPERTIES ---------------------------------------//
        //------------------------------------------------------------------------------------//

        private static string AutoSavesLocation
        {
            get { return EditorPrefs.GetString(_AUTO_SAVER_SAVE_ON_EDITOR_FOLDER_FOLDER, "Auto Saves/Editor"); }
            set { EditorPrefs.SetString(_AUTO_SAVER_SAVE_ON_EDITOR_FOLDER_FOLDER, value); }
        }

        private static bool SaveOriginalWhenSavingBackups
        {
            get { return EditorPrefs.GetBool(_AUTO_SAVER_ON_WHILE_SAVING_BACKUP_AS_WELL, false); }
            set { EditorPrefs.SetBool(_AUTO_SAVER_ON_WHILE_SAVING_BACKUP_AS_WELL, value); }
        }

        private static bool ShouldSaveOnEditorFolder
        {
            get { return EditorPrefs.GetBool(_AUTO_SAVER_SHOULD_SAVE_ON_EDITOR_FOLDER, false); }
            set { EditorPrefs.SetBool(_AUTO_SAVER_SHOULD_SAVE_ON_EDITOR_FOLDER, value); }
        }

        private static bool IsAutoSaverOn
        {
            get { return EditorPrefs.GetBool(_AUTO_SAVER_ON, true); }
            set { EditorPrefs.SetBool(_AUTO_SAVER_ON, value); }
        }

        private static bool AutoSaveBeforeRuntime
        {
            get { return EditorPrefs.GetBool(_AUTO_SAVER_SAVE_BEFORE_RUNTIME, true); }
            set { EditorPrefs.SetBool(_AUTO_SAVER_SAVE_BEFORE_RUNTIME, value); }
        }

        private static int AutoSaveTimer
        {
            get { return EditorPrefs.GetInt(_AUTO_SAVER_TIMER, 5); }
            set { EditorPrefs.SetInt(_AUTO_SAVER_TIMER, value); }
        }

        private static int MaxBackupSceneAmountPerScene
        {
            get { return EditorPrefs.GetInt(_AUTO_SAVER_MAX_BACKUP_AMOUNT, 3); }
            set { EditorPrefs.SetInt(_AUTO_SAVER_MAX_BACKUP_AMOUNT, value); }
        }

        //------------------------------------------------------------------------------------//
        //---------------------------------- METHODS -----------------------------------------//
        //------------------------------------------------------------------------------------//

        static AutoSaver()
        {
            EditorApplication.playModeStateChanged += SaveScene;
            EditorApplication.playModeStateChanged += IsGameRunning;
            UpdateInterval();
            EditorApplication.update += OnUpdate;
            centeredLabelStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = (EditorGUIUtility.isProSkin ? Color.white : Color.black) }
            };
        }

        private static void IsGameRunning(PlayModeStateChange playModeStateChange)
        {
            _isGameRunning = playModeStateChange == PlayModeStateChange.ExitingEditMode ||
                             playModeStateChange == PlayModeStateChange.EnteredPlayMode;
        }

        private static void SaveScene(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingEditMode && AutoSaveBeforeRuntime)
            {
                Debug.Log("Auto-Saving scene before entering Play mode: ".Append(SceneManager.GetActiveScene().name));
                SaveScene();
            }
        }

        private static void UpdateInterval()
        {
            _updateInterval = new TimeSpan(0, AutoSaveTimer, 0);
        }

        private static void SaveScene()
        {
            if (!IsAutoSaverOn || _isGameRunning) { return; }

            if (ShouldSaveOnEditorFolder)
            {
                SaveBackupScene();
                if (SaveOriginalWhenSavingBackups)
                {
                    EditorSceneManager.SaveOpenScenes();
                }
            }
            else
            {
                EditorSceneManager.SaveOpenScenes();
            }
        }

        private static string GetBackupSceneName(string originalSceneName)
        {
            var scene = Path.GetFileNameWithoutExtension(originalSceneName);
            return string.Format("{0}_Backup_{1}.unity", scene, System.DateTime.Now.ToString(_DATE_TIME_STRING_FORMAT, CultureInfo.InvariantCulture));
        }

        private static void SaveBackupScene()
        {
            EnsureAutoSavePathExists();

            string originalSceneName = SceneManager.GetActiveScene().name;
            string newName = GetBackupSceneName(originalSceneName);
            string folder = Path.Combine("Assets", AutoSavesLocation);

            AttemptToDeleteOldestScene(originalSceneName, folder);
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), Path.Combine(folder, newName), true);
            AssetDatabase.SaveAssets();
        }

        private static void AttemptToDeleteOldestScene(string originalName, string path)
        {
            List<string> scenes = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => s.Contains(".unity") && s.Contains(originalName) && !s.Contains(".meta")).ToList();

            if (scenes.Count < MaxBackupSceneAmountPerScene) { return; }

            DeleteOldScenes(scenes);
        }

        private static void DeleteOldScenes(List<string> scenes)
        {
            List<DateTime> dates = new List<DateTime>();
            Dictionary<DateTime, int> indexes = new Dictionary<DateTime, int>();
            for (int i = 0; i < scenes.Count; i++)
            {
                string currentScene = scenes[i];
                DateTime date = DateTime.ParseExact(currentScene.Substring(currentScene.Length - _DATE_TIME_STRING_FORMAT.Length - 6, _DATE_TIME_STRING_FORMAT.Length), _DATE_TIME_STRING_FORMAT, CultureInfo.InvariantCulture);
                dates.Add(date);
                indexes.Add(date, i);
            }

            dates = dates.OrderBy(date => date.Date).ToList();

            while (dates.Count >= MaxBackupSceneAmountPerScene)
            {
                DeleteScene(scenes[indexes[dates[0]]]);
                dates.RemoveAt(0);
            }

        }

        private static void DeleteScene(string scenePath)
        {
            AssetDatabase.DeleteAsset(scenePath);
        }

        private static void DeleteAllBackupScenes()
        {
            List<string> scenes = Directory.GetFiles(Path.Combine("Assets", AutoSavesLocation), "*.*", SearchOption.AllDirectories)
                .Where(s => s.Contains(".unity") && !s.Contains(".meta")).ToList();

            foreach (string scene in scenes)
            {
                DeleteScene(scene);
            }
        }

        private static void EnsureAutoSavePathExists()
        {
            var path = Path.Combine(Application.dataPath, AutoSavesLocation);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void OnUpdate()
        {
            if ((System.DateTime.Now - _lastSaveTime) >= _updateInterval)
            {
                SaveScene();
                _lastSaveTime = System.DateTime.Now;
            }
        }

        [PreferenceItem("Auto Saver")]
        public static void PreferencesGUI()
        {
            EditorGUILayout.LabelField("(Hover On Any Option For A Detailed Explanation)", centeredLabelStyle);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Master Switch", EditorStyles.boldLabel);
            // Is Auto Save On?
            IsAutoSaverOn = EditorGUILayout.Toggle(new GUIContent("Is Auto Saver On?", "If on, the auto saving tool will do its work. An enable/disable option."), IsAutoSaverOn);
            if (!IsAutoSaverOn) { return; }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            // Auto Save Before Runtime?
            AutoSaveBeforeRuntime = EditorGUILayout.Toggle(new GUIContent("Auto Save Before Runtime?",
                "Should the tool auto save the scene before going into runtime mode?\n\n(This will esentially disable the built-in Unity prompt that asks you that, and auto respond \"Yes, Save Scene\" for you.)"),
                AutoSaveBeforeRuntime);

            // Auto Save Interval
            EditorGUI.BeginChangeCheck();
            AutoSaveTimer = EditorGUILayout.IntField(new GUIContent("Minutes Between Auto Saves", "Time In Minutes Before Each Auto Save. If used in conjunction with backup scenes, it is suggested to increase time."), AutoSaveTimer);
            if (EditorGUI.EndChangeCheck()) { UpdateInterval(); }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Backup Scene Settings", EditorStyles.boldLabel);

            // Create A Copy When Auto Saving?
            ShouldSaveOnEditorFolder = EditorGUILayout.Toggle(new GUIContent("Auto Save As A Backup Scene?",
                "When enabled, any changes will be saved on a different copy of the same scene.\n\nThe original scene will NOT be saved, but a copied scene with current changes will be added to the specified folder.")
                , ShouldSaveOnEditorFolder);

            if (ShouldSaveOnEditorFolder)
            {
                // Max Backup Scenes
                MaxBackupSceneAmountPerScene = EditorGUILayout.IntField(new GUIContent("Max Backup Scenes (Per Scene)",
                        "Amount Of Auto-Saved Scenes that will be allowed to be on the specified directory before deleting.\n\nThis is PER scene. If you set to 3, the folder can have up to 3 times the amount of scenes you work on, as each of those can have up to 3 backups.\n\nThe oldest file will always be deleted first."),
                    MaxBackupSceneAmountPerScene);

                // Backup Scenes Location
                EditorGUILayout.LabelField(new GUIContent("Save Backup Scenes At:",
                    "Folder where backup scenes should be saved at. It is recommended that they are added on an \"Editor\" folder so they are excluded from builds.")
                    , centeredLabelStyle);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent("Assets/",
                    "Folder where backup scenes should be saved at. It is recommended that they are added on an \"Editor\" folder so they are excluded from builds."),
                    GUILayout.MaxWidth(45));
                AutoSavesLocation = EditorGUILayout.TextField(AutoSavesLocation);
                EditorGUILayout.EndHorizontal();

                if (!SaveOriginalWhenSavingBackups)
                {
                    EditorGUILayout.HelpBox("WARNING! When saving backup files, the original scene ISN'T being saved!\n\nTo save the original scene as well, check the box below.", MessageType.Warning);
                }

                // Save Original Scene As Well?
                SaveOriginalWhenSavingBackups = EditorGUILayout.Toggle(new GUIContent("Save Original As Well?",
                        "When saving backup scenes, the original scene is never saved. That is, unless you check this box.")
                    , SaveOriginalWhenSavingBackups);

            }// End of ShouldSaveOnEditorFolder

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Delete ALL Backup Scenes") &&
               EditorUtility.DisplayDialog("Are You Sure?", "Do you really want to delete ALL backup scenes?\n\nThis will affect ALL backups, not just only those from the active scene.", "Yes, I'm Sure.", "Changed My Mind."))
            {
                DeleteAllBackupScenes();
            }
        }
    }

}
