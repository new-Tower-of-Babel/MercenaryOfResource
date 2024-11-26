using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Monster
{

    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        MoveToPlayer();
        if (playerDistance < data.attackDistance)
        {
            Attack();
        }

    
    }

}
