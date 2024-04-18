using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase
{
    protected MonoBehaviourCallbacks behaviour;

    public GameObject gameObject;

    public Transform transform => gameObject.transform;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void SwapModel(string name)
    {
        Transform originalParent = transform.parent;
        Vector3 originalPosition = transform.position;
        Quaternion originalRotation = transform.rotation;

        Object.Destroy(gameObject);

        gameObject = Object.Instantiate(ModelManager.Load(name));
        behaviour = gameObject.AddComponent<MonoBehaviourCallbacks>();

        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        gameObject.transform.parent = originalParent;

        behaviour.awake = Awake;
        behaviour.start = Start;
        behaviour.update = Update;

        gameObject.name = gameObject.name.Replace("(Clone)", "");
    }

    public ModelBase(string name)
    {
        gameObject = Object.Instantiate(ModelManager.Load(name));
        behaviour = gameObject.AddComponent<MonoBehaviourCallbacks>();

        behaviour.awake = Awake;
        behaviour.start = Start;
        behaviour.update = Update;

        gameObject.name = gameObject.name.Replace("(Clone)", "");
    }

    public ModelBase(string name, Transform parent)
    {
        gameObject = Object.Instantiate(ModelManager.Load(name));
        gameObject.transform.parent = parent;

        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;

        behaviour = gameObject.AddComponent<MonoBehaviourCallbacks>();

        behaviour.awake = Awake;
        behaviour.start = Start;
        behaviour.update = Update;

        gameObject.name = gameObject.name.Replace("(Clone)", "");
    }

    public ModelBase(string name, Vector3 position)
    {
        gameObject = Object.Instantiate(ModelManager.Load(name));
        gameObject.transform.position = position;

        behaviour = gameObject.AddComponent<MonoBehaviourCallbacks>();

        behaviour.awake = Awake;
        behaviour.start = Start;
        behaviour.update = Update;

        gameObject.name = gameObject.name.Replace("(Clone)", "");
    }

    public ModelBase(string name, Vector3 position, Quaternion rotation)
    {
        gameObject = Object.Instantiate(ModelManager.Load(name));

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;

        behaviour = gameObject.AddComponent<MonoBehaviourCallbacks>();

        behaviour.awake = Awake;
        behaviour.start = Start;
        behaviour.update = Update;

        gameObject.name = gameObject.name.Replace("(Clone)", "");
    }
}
