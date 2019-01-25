using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{   
    public int Health { get; set; }

    //for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
        

    }

    public void Damage()
    {
        if (isDeath == false)
        {
            Health--;
            anim.SetTrigger("Hit");
            isHit = true;
            anim.SetBool("InCombat", true);
            if (Health < 1)
            {
                isDeath = true;
                anim.SetTrigger("Death");
                GameObject diamond = Instantiate(diamondPrefab,transform.position, Quaternion.identity) as GameObject;
                diamond.GetComponent<Diamond>().gems = base.gems;
            }
        }
    }


}
