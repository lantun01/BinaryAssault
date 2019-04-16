using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemyWaveManager))]
public class EnemyWaveEditor : Editor
{
    readonly GUIStyle style = new GUIStyle();


    private void OnEnable()
    {
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;
    }

    public void OnSceneGUI()
    {
        EnemyWaveManager manager = (EnemyWaveManager)target;
        foreach (EnemyWave wave in manager.waves)
        {
            foreach (EnemySpawn spawn in wave.enemigos)
            {
                int id = 0;
                var field = spawn.GetType().GetField("spawnPosition");
                spawn.spawnPosition = Handles.FreeMoveHandle(spawn.spawnPosition, Quaternion.identity, 0.5f, Vector2.zero, Handles.CylinderHandleCap);
                Handles.color = Color.red;
                Handles.Label(spawn.spawnPosition, "Enemigo", style);

            }
        }
    }

    //public override void OnInspectorGUI()
    //{
    //  //  base.OnInspectorGUI();
    //    serializedObject.Update();
    //    var waves = serializedObject.FindProperty("waves");
    //    EditorList.Show(waves,EditorListOption.Default);
    //    serializedObject.ApplyModifiedProperties();


    //}
    
}
