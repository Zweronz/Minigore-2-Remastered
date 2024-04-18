using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoundManager : MonoBehaviour
{
    public List<Sound> sounds;

    [System.Serializable]
    public class Sound
    {
        public string path;

        public AudioClip clip;
    }

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("SoundManager").GetComponent<SoundManager>();
            }

            return instance;
        }
    }

    public static AudioClip Load(string path)
    {
        return Instance.sounds.Find(x => x.path == path).clip;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundManager))]
public class SoundManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sounds"));

        if (GUILayout.Button("Load"))
        {
            ((SoundManager)target).sounds = (from guid in AssetDatabase.FindAssets("t: AudioClip") select new SoundManager.Sound { clip = AssetDatabase.LoadAssetAtPath<AudioClip>(AssetDatabase.GUIDToAssetPath(guid)), path = AssetDatabase.GUIDToAssetPath(guid).Replace("Assets/", "").Replace(".ogg", "") } ).ToList();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif