
/*---------------- Creation Date: 18-Dec-17 -----------------//
//------------ Last Modification Date: 18-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Selection History.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Class that keeps track of the selected items in Unity.
 *
 *   <<< LIMITATIONS >>>
 *       -- Currently, the selections from scenes get deleted when loading a new one, and NO selection persists between sessions.
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
 *       -- Make a system to save selections before closing the editor.
 *
 *   <<< SOURCES >>>
 *       -- [1] Base class : https://pastebin.com/V9kkemiu
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Dec-17 >>>
 *       -- Created using the reference. [1]
 *       -- Improved by allowing to show/hide scene/project elements as wanted. 
 *       -- Improved by adding a little toggle to ignore new selections if needed.
 *       -- Improved by making the scene elements more noticeable on the "Any" view, by adding the name of the active scene.
 *       -- Made both above settings save themselves on EditorPrefs.
//----------------------------------------------------------------------*/

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace PowerTools
{

    public class SelectionHistory : EditorWindow
    {
        //------------------------------------------------------------------------------------//
        //--------------------------- CLASSES DECLARATIONS -----------------------------------//
        //------------------------------------------------------------------------------------//

        [System.Serializable]
        class SelectedObject
        {
            public bool locked = false;
            public Object selection = null;
            public bool inScene = false;
        }

        //------------------------------------------------------------------------------------//
        //----------------------------- ENUM DECLARATIONS ------------------------------------//
        //------------------------------------------------------------------------------------//

        private enum EVisibleElements
        {
            Any = 0,
            Scene = 1,
            Project = 2
        }

        //------------------------------------------------------------------------------------//
        //----------------------------------- FIELDS -----------------------------------------//
        //------------------------------------------------------------------------------------//


        private const int _MAX_ITEMS = 50;
        private const int _MAX_SCENE_NAME_SIZE = 15;
        private const string _SCENE_ITEM_APPENDIX = "*";
        private const string _SELECTION_LOG_IGNORE_NEW_SELELCTIONS = "_SELECTION_LOG_IGNORE_NEW_SELELCTIONS";
        private const string _SELECTION_LOG_VISIBLE_ITEMS = "_SELECTION_LOG_VISIBLE_ITEMS";

        [SerializeField]
        private SelectedObject _selectedObject = null;
        [SerializeField]
        private List<SelectedObject> _selectedObjects = new List<SelectedObject>();

        [SerializeField]
        private Vector2 _scrollPosition = Vector2.zero;
        [SerializeField]
        private static EVisibleElements _visibleElements;
        [SerializeField]
        private static bool _ignoreNewSelections;

        [System.NonSerialized]
        private GUIStyle _lockButtonStyle;
        [System.NonSerialized]
        private static GUIStyle _centeredLabelStyle;
        [System.NonSerialized]
        private GUIContent _searchButtonContent = null;

        //------------------------------------------------------------------------------------//
        //---------------------------------- METHODS -----------------------------------------//
        //------------------------------------------------------------------------------------//

        #region Menu Items

        // Add menu item named "Super Animation Editor" to the Window menu
        [MenuItem("Tools/Utilities/Selection History/Open Window")]
        static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow<SelectionHistory>("Selection History");
        }

        [MenuItem("Tools/Utilities/Selection History/Toggle Ignore New Selections %&s")]
        static void ToggleIgnore()
        {
            _ignoreNewSelections = !_ignoreNewSelections;
            EditorUtility.SetDirty(EditorWindow.GetWindow<SelectionHistory>(false, "Selection History", true));
        }

        #endregion Menu Items

        #region Preferences

        private void OnEnable()
        {
            _ignoreNewSelections = EditorPrefs.GetBool(_SELECTION_LOG_IGNORE_NEW_SELELCTIONS, false);
            _visibleElements = (EVisibleElements)EditorPrefs.GetInt(_SELECTION_LOG_VISIBLE_ITEMS, 0);
        }

        private void OnDisable()
        {
            SavePreferences();
        }

        void SavePreferences()
        {
            EditorPrefs.SetBool(_SELECTION_LOG_IGNORE_NEW_SELELCTIONS, _ignoreNewSelections);
            EditorPrefs.SetInt(_SELECTION_LOG_VISIBLE_ITEMS, (int)_visibleElements);
        }

        #endregion Preferences

        #region Window Drawing

        void OnSelectionChange()
        {
            if (Selection.activeObject == null || _ignoreNewSelections)
            {
                _selectedObject = null;
            }
            else if (_selectedObject == null || _selectedObject.selection != Selection.activeObject)
            {
                // If the object's in the list, select it, and if not locked, move it to the top of the list
                SelectedObject obj = _selectedObjects.Find(item => item.selection == Selection.activeObject);

                _selectedObject = obj;

                if (obj != null)
                {
                    _selectedObjects.Remove(obj);
                    int firstNonLocked = (_selectedObjects.FindIndex(item => item.locked == true));
                    if (firstNonLocked >= 0)
                        _selectedObjects.Insert(firstNonLocked, obj);
                    else
                        _selectedObjects.Add(obj);
                    _selectedObject = obj;
                }
                else
                {

                    // Find first non-locked item
                    obj = new SelectedObject()
                    {
                        selection = Selection.activeObject,
                        inScene = AssetDatabase.Contains(Selection.activeInstanceID) == false
                    };
                    int firstNonLocked = (_selectedObjects.FindIndex(item => item.locked == true));
                    if (firstNonLocked >= 0)
                    {
                        _selectedObjects.Insert(firstNonLocked, obj);
                    }
                    else
                    {
                        _selectedObjects.Add(obj);
                    }
                    _selectedObject = obj;

                }

                // Cap number of items to reasonable amount
                while (_selectedObjects.Count > _MAX_ITEMS)
                    _selectedObjects.RemoveAt(0);

                Repaint();
            }
        }

        void OnGUI()
        {

            if (_centeredLabelStyle == null)
            {
                _centeredLabelStyle = new GUIStyle();
                _centeredLabelStyle.alignment = TextAnchor.MiddleCenter;
                _centeredLabelStyle.normal.textColor = (EditorGUIUtility.isProSkin ? Color.white : Color.black);
            }

            GUILayout.Space(5);

            EditorGUI.BeginChangeCheck();
            _ignoreNewSelections = EditorGUILayout.ToggleLeft(new GUIContent("Ignore New Selections (CTRL+ALT+S)"), _ignoreNewSelections);

            EditorGUILayout.LabelField("Visible Selections:", _centeredLabelStyle);
            _visibleElements = (EVisibleElements)GUILayout.Toolbar((int)_visibleElements, new string[] { "Any", "Active Scene", "Project" });
            if (EditorGUI.EndChangeCheck())
            {
                SavePreferences();
            }


            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            GUILayout.Space(5);

            bool shownClear = false;
            bool processingLocked = false;
            for (int i = _selectedObjects.Count - 1; i >= 0; --i)
            {
                if (_selectedObjects[i].locked == false)
                {
                    if (processingLocked)
                    {
                        // First non-locked. so add Clear button

                        shownClear = true;
                        if (LayoutClearButton())
                            break;
                        processingLocked = false;
                    }
                }
                else
                {
                    processingLocked = true;
                }

                LayoutItem(i, _selectedObjects[i]);
            }

            // If clear button hasn't shown above (ie: no locked items), show it below
            if (shownClear == false)
                LayoutClearButton();

            EditorGUILayout.EndScrollView();

        }

        bool LayoutClearButton()
        {
            GUILayout.Space(5);

            bool clear = GUILayout.Button("Clear", EditorStyles.miniButton);
            if (clear)
            {
                for (int j = _selectedObjects.Count - 1; j >= 0; --j)
                {
                    if (_selectedObjects[j].locked == false)
                        _selectedObjects.RemoveAt(j);
                }
            }

            GUILayout.Space(5);
            return clear;
        }

        void LayoutItem(int i, SelectedObject obj)
        {

            // Lazy create and cache lock button style
            if (_lockButtonStyle == null)
            {
                GUIStyle temp = "IN LockButton";
                _lockButtonStyle = new GUIStyle(temp);
                _lockButtonStyle.margin.top = 3;
                _lockButtonStyle.margin.left = 10;
                _lockButtonStyle.margin.right = 10;
            }

            GUIStyle style = EditorStyles.miniButtonLeft;
            style.alignment = TextAnchor.MiddleLeft;

            if (obj != null && obj.selection != null)
            {
                bool shouldBeSeen = _visibleElements == EVisibleElements.Any ||
                                    obj.inScene && _visibleElements == EVisibleElements.Scene ||
                                    !obj.inScene && _visibleElements == EVisibleElements.Project;

                if (!shouldBeSeen)
                {
                    return;
                }

                GUILayout.BeginHorizontal();

                bool wasLocked = obj.locked;
                obj.locked = GUILayout.Toggle(obj.locked, GUIContent.none, _lockButtonStyle);
                if (wasLocked != obj.locked)
                {

                    _selectedObjects.Remove(obj);
                    int firstNonLocked = (_selectedObjects.FindIndex(item => item.locked == true));

                    if (firstNonLocked >= 0)
                    {
                        _selectedObjects.Insert(firstNonLocked, obj);
                    }
                    else
                    {
                        _selectedObjects.Add(obj);
                    }

                }

                if (obj == _selectedObject)
                {
                    GUI.enabled = false;
                }

                string objName = obj.selection.name;
                if (obj.inScene)
                {
                    string sceneName = SceneManager.GetActiveScene().name;
                    objName = string.Format("{0}{1}{2}",
                        objName, // Base Name
                        _visibleElements == EVisibleElements.Any ? _SCENE_ITEM_APPENDIX : string.Empty, // "Is In Scene Mark" (If Needed)
                                                                                                        // Scene Name (If Needed, Could Get Trimmed If Too Long)
                        _visibleElements == EVisibleElements.Any ? string.Format(" - (Scene: \"{0}{1}\")", sceneName.Substring(0, sceneName.Length <= _MAX_SCENE_NAME_SIZE ? sceneName.Length : _MAX_SCENE_NAME_SIZE),
                        sceneName.Length <= _MAX_SCENE_NAME_SIZE ? string.Empty : "...") : string.Empty);
                }
                if (GUILayout.Button(objName, style))
                {
                    _selectedObject = obj;
                    Selection.activeObject = obj.selection;
                }

                GUI.enabled = true;

                // Lazy find and cache Search button
                if (_searchButtonContent == null)
                    _searchButtonContent = EditorGUIUtility.IconContent("d_ViewToolZoom");

                if (GUILayout.Button(_searchButtonContent, EditorStyles.miniButtonRight, GUILayout.MaxWidth(25), GUILayout.MaxHeight(15)))
                {
                    EditorGUIUtility.PingObject(obj.selection);
                }

                GUILayout.EndHorizontal();
            }
        }

        #endregion Window Drawing

    }

}