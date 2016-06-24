#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyPaths))]
public class EnemyPathsEditor : Editor {

	void OnSceneGUI()
    {
        EnemyPaths me = (EnemyPaths)target;


    }

}
#endif