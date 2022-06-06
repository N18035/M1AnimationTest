using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public AnimationClip clip;
    public AnimationClip clip2;

    // Start is called before the first frame update
    void Start()
    {
        clip = new AnimationClip();
        clip2 = new AnimationClip();
    }
}
