using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    void Start()
    {
        new SunnyLake();
        JohnGore character = new JohnGore(new Vector3(5, -2.953773f, 100), Quaternion.identity);
        CameraController.target = character.transform;
        new MachineGun(character);
        new MetusalemIntro();
    }
}
