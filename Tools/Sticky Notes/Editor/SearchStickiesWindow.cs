
/*---------------- Creation Date: 18-Dec-17 -----------------//
//------------ Last Modification Date: 18-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Search Stickies Window.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Editor window that allows searching within a scene all sticky notes.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module:
 *          -- StickyNote
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Base class : https://github.com/charblar/stickies
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Dec-17 >>>
 *       -- Added class based on the reference [1].
 *       -- Improved class by adding an option to add a new note in the scene.
 *       -- Improved overall visibility of the different elements of the window.
//----------------------------------------------------------------------*/

using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SearchStickiesWindow : EditorWindow
{
    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    private enum SearchType { Title, Color }
    private SearchType _searchBy;

    private string _searchTitle;
    private StickyNote.NoteColor _searchColor;

    private Vector2 _scrollPosition = Vector2.zero;
    private List<StickyNote> _matchingNotes = null;
    private HashSet<int> _deletedNotes = new HashSet<int>();

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [MenuItem("Tools/Scene/Sticky Notes/Search Notes", priority = 100)]
    public static void ShowWindow()
    {
        GetWindow<SearchStickiesWindow>(false, "Search Notes", true);
    }

    void DrawSearchButton()
    {
        if (GUILayout.Button("Search In Scene"))
            PerformSearch();
    }

    void DrawBottomSection()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Search"))
            _matchingNotes = null;
        if (GUILayout.Button("+", GUILayout.Width(19f)))
            CreateNewNoteInScene();
        EditorGUILayout.EndHorizontal();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        _searchBy = (SearchType)EditorGUILayout.EnumPopup("Search by", _searchBy);
        EditorGUILayout.EndHorizontal();

        //display proper gui for current search type
        switch (_searchBy)
        {
            case (SearchType.Title):
                EditorGUILayout.BeginHorizontal();
                _searchTitle = EditorGUILayout.TextField(_searchTitle);
                DrawSearchButton();
                EditorGUILayout.EndHorizontal();
                break;
            case (SearchType.Color):
                EditorGUILayout.BeginHorizontal();
                _searchColor = (StickyNote.NoteColor)EditorGUILayout.EnumPopup(_searchColor);
                DrawSearchButton();
                EditorGUILayout.EndHorizontal();
                break;
        }

        EditorGUILayout.Space();

        //display search results
        if (_matchingNotes != null)
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, false, false);
            EditorGUILayout.BeginVertical(GUI.skin.box);
            if (_matchingNotes.Count > 0)
            {
                EditorGUILayout.LabelField(_matchingNotes.Count.ToString() + " results found!");

                _deletedNotes.Clear();
                for (int i = 0; i < _matchingNotes.Count; i++)
                {
                    if (_matchingNotes[i] == null)
                    {
                        _deletedNotes.Add(i);
                        continue;
                    }
                    DisplayNote(_matchingNotes[i]);
                }
                foreach (int i in _deletedNotes)
                {
                    _matchingNotes.RemoveAt(i);
                }
            }
            else
            {
                EditorGUILayout.LabelField("No results found :(");
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
        }

        DrawBottomSection();
    }

    void CreateNewNoteInScene()
    {
        StickyNote.CreateNote();
    }

    void PerformSearch()
    {
        _matchingNotes = new List<StickyNote>();

        StickyNote[] allNotes = FindObjectsOfType<StickyNote>();

        switch (_searchBy)
        {
            case (SearchType.Title):
                foreach (StickyNote n in allNotes)
                {
                    if (n.Title.IndexOf(_searchTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                        _matchingNotes.Add(n);
                }
                break;
            case (SearchType.Color):
                foreach (StickyNote n in allNotes)
                {
                    if (n.Color == _searchColor)
                        _matchingNotes.Add(n);
                }
                break;
        }

        //sort results first to last
        _matchingNotes.Reverse();
    }

    void DisplayNote(StickyNote stickyNote)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(stickyNote.Title.Equals(string.Empty) ? string.Format("Untitled: \"{0}\" (GO Name)", stickyNote.name) : stickyNote.Title);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Select"))
        {
            if (stickyNote)
                Selection.activeObject = stickyNote;
            else
                Debug.LogWarning("Note does not exist, did you delete it?");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

}