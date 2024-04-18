using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AnimatedModel
{
    protected CharacterController controller;

    protected Weapon weapon;

    protected virtual float radius
    {
        get
        {
            return 10f;
        }
    }

    protected virtual float height
    {
        get
        {
            return 32.5f;
        }
    }

    protected virtual float speed
    {
        get
        {
            return 75f;
        }
    }

    protected Vector3 center
    {
        get
        {
            return new Vector3(-0.5f, 19.5f, 0);
        }
    }

    protected virtual Vector2 GetCharacterInput()
    {
        Vector2 input = new Vector2(speed, speed);

        if (!Input.GetKey(KeyCode.D))
        {
            input.x -= speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x -= speed;
        }

        if (!Input.GetKey(KeyCode.W))
        {
            input.y -= speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y -= speed;
        }

        return input;
    }

    protected override void Update()
    {
        base.Update();

        Vector2 input = -GetCharacterInput();

        if (input.x != 0 && input.y != 0)
        {
            input /= 1.5f;
        }

        looping = input != Vector2.zero;

        if (weapon != null)
        {
            weapon.looping = looping;
        }

        if (looping)
        {
            controller.Move(new Vector3(input.x * Time.deltaTime, -1, input.y * Time.deltaTime));

            controller.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(input.x / speed, input.y / speed) * Mathf.Rad2Deg, 0);
        }
    }

    public virtual void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public Character(string name, Vector3 position, Quaternion rotation) : base (name, position, rotation)
    {
        controller = gameObject.AddComponent<CharacterController>();

        controller.radius = radius;
        controller.height = height;

        controller.center = center;
    }
}
