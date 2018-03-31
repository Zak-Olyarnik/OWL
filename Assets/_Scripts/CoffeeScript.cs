using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeScript : EnemyController {

    protected override void attack()
    {
        PlayerController.GetInstance().Damage(damage);
        DestroyObject(this.gameObject);
    }
}
