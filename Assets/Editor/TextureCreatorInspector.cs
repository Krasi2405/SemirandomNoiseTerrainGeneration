using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextureGenerator))]
public class TextureCreatorInspector : Editor
{

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector();
	}
}
