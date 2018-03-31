using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

    //Animation
    public void die()
    {
        anim.SetTrigger("dead");
    }

    public void setSpeed(float speed)
    {
        anim.SetFloat("speed", speed);
    }

    public void attackEnd()
    {
        anim.SetBool("attacking", false);
    }

    public void attackStart()
    {
        anim.SetBool("attacking",true);
    }

    //Callbacks
    public float getSpeed(float speed)
    {
        return anim.GetFloat("speed");
    }

    public bool getAttacking()
    {
        return anim.GetBool("attacking");
    }

    public bool actuallyDie()
    {
        return anim.GetBool("actuallyDead");
    }

    public void setDead()
    {
        anim.SetBool("actuallyDead", true);
    }
}
