using UnityEngine;
using System.Collections;
using UnityEditor;

public class NoteEditorMenu : MonoBehaviour
{
	[MenuItem("Component/Note/Add note")]
	static void AddNoteToGameObject ()
	{
		Selection.activeGameObject.AddComponent (typeof(Note));
	}

	[MenuItem("Component/Note/Add note", true)]
	static bool AddNoteToGameObjectValidation ()
	{
		return Selection.activeTransform != null;
	}

}

[CustomEditor(typeof(Note))]
public class NoteEditor : Editor {
	private SerializedProperty note;
	private bool editable = false;
	private GUILayoutOption[] options = {
		GUILayout.ExpandWidth (true), 
		GUILayout.ExpandHeight (true)
	};
	
	private void OnEnable ()
	{
		note = serializedObject.FindProperty ("note");
		editable = false;
	}
	
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		
		GUIStyle textAreaStyle = GUI.skin.GetStyle ("textArea");
		textAreaStyle.wordWrap = true;
		textAreaStyle.stretchWidth = true;	
		textAreaStyle.fixedHeight = 0;
		textAreaStyle.stretchHeight = true;
		
		GUIStyle labelStyle = GUI.skin.GetStyle ("label");
		labelStyle.wordWrap = true;
		labelStyle.stretchWidth = true;	

		if (GUILayout.Button (editable ? "Lock note" : "Edit note")) {
			editable = !editable;
		}
		
		GUILayout.Label ("Note:");
						
		if (editable) {		
			note.stringValue = EditorGUILayout.TextArea (note.stringValue, 
				textAreaStyle,
				options);
		} else {
			EditorGUILayout.LabelField (note.stringValue, 
				labelStyle,
				options);
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}