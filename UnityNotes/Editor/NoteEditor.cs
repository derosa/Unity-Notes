using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Note))]
public class NoteEditor : Editor {
	private SerializedProperty noteContent;
	private SerializedProperty noteTitle;
	private bool editable = false;
	private GUILayoutOption[] options = {
		GUILayout.ExpandWidth (true), 
		GUILayout.ExpandHeight (true)
	};
	
	private void OnEnable ()
	{
		noteContent = serializedObject.FindProperty ("note");
		noteTitle = serializedObject.FindProperty ("title");
		editable = false;
	}
	
	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();
		
		GUIStyle textAreaStyle = new GUIStyle(GUI.skin.GetStyle ("textArea"));
		textAreaStyle.wordWrap = true;
		textAreaStyle.stretchWidth = true;	
		textAreaStyle.fixedHeight = 0;
		textAreaStyle.stretchHeight = true;
		
		GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle ("label"));
		labelStyle.wordWrap = true;
		labelStyle.stretchWidth = true;	
		labelStyle.fontStyle = FontStyle.Normal;

		GUIStyle labelBoldStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
		labelBoldStyle.fontStyle = FontStyle.Bold;

		if (GUILayout.Button (editable ? "Lock note" : "Edit note")) {
			editable = !editable;
		}
						
		if (editable) {	
			GUILayout.Label ("Title", labelBoldStyle);
			noteTitle.stringValue = EditorGUILayout.TextField (noteTitle.stringValue);
			GUILayout.Label ("Note", labelBoldStyle);
			noteContent.stringValue = EditorGUILayout.TextArea (noteContent.stringValue, 
				textAreaStyle,
				options);
		} else {
			GUILayout.Label ("Title", labelBoldStyle);
			EditorGUILayout.LabelField (noteTitle.stringValue, labelStyle);
			GUILayout.Label ("Note", labelBoldStyle);
			EditorGUILayout.LabelField (noteContent.stringValue, 
				labelStyle,
				options);
		}
		
		serializedObject.ApplyModifiedProperties ();
	}
}