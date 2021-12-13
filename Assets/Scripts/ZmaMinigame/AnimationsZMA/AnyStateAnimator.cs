using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void AnimationTriggerEvent(string animation);

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

     public AnimationTriggerEvent AnimationTriggerEvent { get; set; }

    private string currentAnimationLegs = string.Empty;

    private string currentAnimationTorso = string.Empty;

    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();
    }

    public void AddAnimations(params AnyStateAnimation[] newAnimations )
    {
        for(int i = 0; i < newAnimations.Length; i++ )
        {
            this.animations.Add(newAnimations[i].Name, newAnimations[i]);
        }
    }

    public void TryToPlayAnimation(string newAnimation)
    {
        switch(animations[newAnimation].AnimationRig)
        {
            case RIG.LEGS:
                PlayAnimation(ref currentAnimationLegs);
                break;
            case RIG.TORSO:
                PlayAnimation(ref currentAnimationTorso);
                break;
            default: Debug.Log("Animation tried");
                break;
        }
      
        void PlayAnimation(ref string currentAnimation)
        {
            if(currentAnimation == "" )
            {
                animations[newAnimation].Active = true;
                currentAnimation = newAnimation;
            }

            else if(currentAnimation != newAnimation && !animations[newAnimation].HigherPrio.Contains(currentAnimation) ||
                    !animations[currentAnimation].Active)
            {
                animations[currentAnimation].Active = false;
                animations[newAnimation].Active = true;
                currentAnimation = newAnimation;
            }
        }
    }

    private void Animate()
    {
        foreach (string key in animations.Keys)
        {
            animator.SetBool(key, animations[key].Active);
        }
    }
    
    public void OnAnimationDone(string animation)
    {
        animations[animation].Active = false;
    }

    public void OnAnimationTrigger(string animation)
    {
        if (animation != null )
            AnimationTriggerEvent.Invoke(animation);
    }
}
