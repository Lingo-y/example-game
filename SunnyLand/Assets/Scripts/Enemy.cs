using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    // Start is called before the first frame update
    protected virtual void Start()//virtual 方便子类重写
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        anim.SetTrigger("Death");
        deathAudio.Play();
    }
}

