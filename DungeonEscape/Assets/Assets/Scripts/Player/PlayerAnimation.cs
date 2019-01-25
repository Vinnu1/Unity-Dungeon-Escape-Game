using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private Animator _swordAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Move(float move)
    {
        //anim set float Move
        _anim.SetFloat("Move", Mathf.Abs(move)); //since move left is -1, we need absolute
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }

    public void Death()
    {
        Debug.Log("Killed: Death Called");
        _anim.SetTrigger("Death");
            
        
    }
}
