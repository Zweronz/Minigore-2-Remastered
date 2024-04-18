using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetusalemIntro : AnimatedModel
{
    protected override void Update()
    {
        if (!animation.isPlaying)
        {
            Object.Destroy(gameObject);
        }
    }

    public MetusalemIntro() : base("MerchantJennyCapture")
    {
        Play();
    }
}
