using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnGore : Character
{
    public override void SetWeapon(Weapon weapon)
    {
        base.SetWeapon(weapon);

        weapon.transform.parent = transform.Find("JohnGore1st_Body");

        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public JohnGore(Vector3 position, Quaternion rotation) : base("JohnGore1st", position, rotation)
    {
    }
}
