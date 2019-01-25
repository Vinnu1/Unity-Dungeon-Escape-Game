using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    public GameObject acidEffectPrefab;

    public void Damage()
    {
        if (isDeath == false)
        {
            Health--;
            if (Health < 1)
            {
                isDeath = true;
                anim.SetTrigger("Death");
                GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
                diamond.GetComponent<Diamond>().gems = base.gems;
            }
        }
    }

    public override void Update()
    {
        
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        //don't have to move
    }

    public void Attack()
    {

        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }

}
