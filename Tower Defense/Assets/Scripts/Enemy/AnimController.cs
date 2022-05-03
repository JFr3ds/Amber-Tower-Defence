using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator anim;

    public void SetAnimator(Anim currentAnimation)
    {
        switch (currentAnimation)
        {
            case Anim.Walk:
                anim.SetTrigger("Walk");
                break;
            case Anim.Run:
                anim.SetTrigger("Run");
                break;
            case Anim.Die:
                anim.SetTrigger("Die");
                break;
        }
    }
}

public enum Anim
{
    Walk,
    Run,
    Die
}
