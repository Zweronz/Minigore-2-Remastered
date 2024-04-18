using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static new Camera camera;

    public static Transform target;

    public Vector3 offset;

    public Vector3 rotationOffset;

    void Awake()
    {
        camera = GetComponent<Camera>();

        camera.transform.position = offset;
        camera.transform.rotation = Quaternion.Euler(rotationOffset);
    }

    void Update()
    {
        if (target != null)
        {
            camera.transform.position = offset + target.transform.position;
        }
    }
}
