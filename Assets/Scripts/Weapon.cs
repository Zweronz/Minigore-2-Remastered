using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : AnimatedModel
{
    public Weapon(string name, Character character) : base(name, character.transform)
    {
        character.SetWeapon(this);
    }
}
