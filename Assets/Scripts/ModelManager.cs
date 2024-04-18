using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ModelManager : MonoBehaviour
{
    public List<GameObject> models;

    private static ModelManager instance;

    public static ModelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("ModelManager").GetComponent<ModelManager>();
            }

            return instance;
        }
    }

    public static GameObject Load(string name)
    {
        return Instance.models.Find(x => x.name == name);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ModelManager))]
public class ModelManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("models"));

        if (GUILayout.Button("Load"))
        {
            ((ModelManager)target).models = (from guid in AssetDatabase.FindAssets("t: Prefab") select AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid))).ToList();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif