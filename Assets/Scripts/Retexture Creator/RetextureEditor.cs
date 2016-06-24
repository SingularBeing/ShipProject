#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Retexture))]
public class RetextureEditor : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		if (GUILayout.Button ("Retexture All in Scene")) {
			
		}
	}

}
#endif