
/*---------------- Creation Date: 18-Dec-17 -----------------//
//------------ Last Modification Date: 18-Dec-17 ------------//
//----------- Luis Arzola: http://heisarzola.com ------------*/

/*----------------------------- OVERVIEW -------------------------------//
 *   <<< NAME >>>
 *       -- Sticky Note.
 *       
 *   <<< DESCRIPTION >>>
 *       -- Component intended to hold information that mimics Window's Sticky Notes.
 *
 *   <<< LIMITATIONS >>>
 *       -- None.
 *
 *   <<< DEPENDENCIES >>>
 *       -- Plugins: None.
 *       -- Module:
 *          -- Gizmos/Stickes icons.
//----------------------------------------------------------------------*/

/*------------------------------- NOTES --------------------------------//
 *   <<< TO-DO LIST >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< POSSIBLES >>>
 *       -- <<< EMPTY >>>
 *
 *   <<< SOURCES >>>
 *       -- [1] Base class : https://github.com/charblar/Sticky Notes
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Dec-17 >>>
 *       -- Added class based on the reference [1].
 *       -- Improved class by forcing any game object with the component to have the tag "EditorOnly".
//----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;

public class StickyNote : MonoBehaviour
{
    //------------------------------------------------------------------------------------//
    //----------------------------- ENUM DECLARATIONS ------------------------------------//
    //------------------------------------------------------------------------------------//

    public enum NoteColor { White, Yellow, Blue, Pink, Green }

    //------------------------------------------------------------------------------------//
    //----------------------------------- FIELDS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [SerializeField] private string _title;
    [SerializeField] private NoteColor _color;
    [Space(5)]
    [SerializeField]
    [TextArea(5, 10)]
    private string _text;

    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    public string Title { get { return _title; } }
    public NoteColor Color { get { return _color; } }

    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [MenuItem("Tools/Scene/Sticky Notes/Create Note", priority = 0)]
    public static void CreateNote()
    {
        var note = new GameObject("New Note", typeof(StickyNote)) { tag = "EditorOnly" };
        Undo.RegisterCreatedObjectUndo(note, "Created Note");
        Selection.activeObject = note;
    }

    private void OnValidate()
    {
        if (!tag.Equals("EditorOnly"))
        {
            tag = "EditorOnly";
        }
    }

    private void OnDrawGizmos()
    {
        switch (_color)
        {
            case NoteColor.White:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_White", true);
                break;
            case NoteColor.Yellow:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Yellow", true);
                break;
            case NoteColor.Blue:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Blue", true);
                break;
            case NoteColor.Pink:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Pink", true);
                break;
            case NoteColor.Green:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Green", true);
                break;
            default:
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_White", true);
                break;
        }
    }
}

