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
        int waveid = 0;
        EnemyWaveManager manager = (EnemyWaveManager)target;
        foreach (EnemyWave wave in manager.waves)
        {
            if (wave.show)
            {
                int enemyid = 0;
                Handles.color = wave.color;
                foreach (EnemySpawn spawn in wave.enemigos)
                {
                    var field = spawn.GetType().GetField("spawnPosition");
                    spawn.spawnPosition = Handles.FreeMoveHandle(spawn.spawnPosition, Quaternion.identity, 0.5f, Vector2.zero, Handles.CylinderHandleCap);
                    Handles.Label(spawn.spawnPosition, "E_"+enemyid+"_"+waveid, style);
                    enemyid++;
                }
            }
            waveid++;

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
