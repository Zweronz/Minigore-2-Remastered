using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedModel : ModelBase
{
    public bool looping = false;

    protected Animation animation;

    protected float time;

    protected override void Update()
    {
        base.Update();

        animation[animation.clip.name].speed = looping ? 1 : 0;

        if (looping && !animation.isPlaying)
        {
            animation.Play();
        }
    }

    protected void Play()
    {
        animation.Play();
    }

    protected override void SwapModel(string name)
    {
        base.SwapModel(name);

        animation = gameObject.GetComponent<Animation>();
        animation.Play();
    }

    public AnimatedModel(string name) : base(name)
    {
        animation = gameObject.GetComponent<Animation>();
        animation.Play();
    }

    public AnimatedModel(string name, Transform parent) : base(name, parent)
    {
        animation = gameObject.GetComponent<Animation>();
        animation.Play();
    }

    public AnimatedModel(string name, Vector3 position) : base(name, position)
    {
        animation = gameObject.GetComponent<Animation>();
        animation.Play();
    }

    public AnimatedModel(string name, Vector3 position, Quaternion rotation) : base(name, position, rotation)
    {
        animation = gameObject.GetComponent<Animation>();
        animation.Play();
    }
}
