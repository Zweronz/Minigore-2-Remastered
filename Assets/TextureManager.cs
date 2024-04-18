using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class TextureManager : MonoBehaviour
{
    public List<Texture2D> textures = new List<Texture2D>();

    private static TextureManager instance;

    public static TextureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("TextureManager").GetComponent<TextureManager>();
            }

            return instance;
        }
    }

    public static Texture2D Load(string name)
    {
        return Instance.textures.Find(x => x.name == name);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TextureManager))]
public class TextureManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("textures"));

        if (GUILayout.Button("Load"))
        {
            ((TextureManager)target).textures = (from guid in AssetDatabase.FindAssets("t: Texture2D") select AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid))).ToList();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif