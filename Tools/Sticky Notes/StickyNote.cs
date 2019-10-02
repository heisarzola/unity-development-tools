
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
 *       -- [1] Base class : https://github.com/charblar/stickies
//---------------------------------------------------------------------*/

/*---------------------------- CHANGELOG -------------------------------//
 *   <<< V.1.0.0 -- 18-Dec-17 >>>
 *       -- Added class based on the reference [1].
 *       -- Improved class by forcing any game object with the component to have the tag "EditorOnly".
//----------------------------------------------------------------------*/

using UnityEditor;
using UnityEngine;

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

    [SerializeField] private bool _showTitle = false;
    [Range(0, 100)]
    [SerializeField] private int _fontSize = 18;
    [SerializeField] private Vector2 _textOffset;
    [Space(5)]
    [SerializeField]
    [TextArea(5, 10)]
    private string _text;
    GUIStyle _style = new GUIStyle();

    //------------------------------------------------------------------------------------//
    //--------------------------------- PROPERTIES ---------------------------------------//
    //------------------------------------------------------------------------------------//

    public string Title { get { return _title; } }
    public NoteColor Color { get { return _color; } }
    public int FontSize { get { return _fontSize; } }
    public Vector2 TextOffset { get { return _textOffset; } }
    public bool ShowTitle { get { return _showTitle; } }


    //------------------------------------------------------------------------------------//
    //---------------------------------- METHODS -----------------------------------------//
    //------------------------------------------------------------------------------------//

    [MenuItem("GameObject/Create Note", priority = 0)]
    public static void CreateNote()
    {
        GameObject parent = Selection.activeGameObject;
        var note = new GameObject("New Note", typeof(StickyNote)) { tag = "EditorOnly" };
        note.transform.SetParent(parent.transform);
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
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.white);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_White", true);
                break;
            case NoteColor.Yellow:
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.yellow);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Yellow", true);
                break;
            case NoteColor.Blue:
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.blue);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Blue", true);
                break;
            case NoteColor.Pink:
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.magenta);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Pink", true);
                break;
            case NoteColor.Green:
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.green);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_Green", true);
                break;
            default:
                if (ShowTitle)
                {
                    DrawTitle(UnityEngine.Color.white);
                }
                Gizmos.DrawIcon(transform.position, "Sticky Notes/Gizmos_Note_White", true);
                break;
        }
    }

    private void DrawTitle(Color c)
    {
        _style.fontSize = FontSize;
        _style.normal.textColor = c;
        Handles.Label(new Vector3(transform.position.x + TextOffset.x, transform.position.y + TextOffset.y),
            Title, _style);
    }


    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            CreateNote();
        }
    }
}

