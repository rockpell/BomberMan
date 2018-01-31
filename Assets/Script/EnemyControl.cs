using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : Unit {

    // Use this for initialization
    void Start () {
        life_point = 2;
        is_non_damage = false;

        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        randDirection();
    }
	
	// Update is called once per frame
	void Update () {
        if(life_point > 0) {

        } else {
            die();
        }
    }

    void randDirection() {
        direction = (unit_direction)Random.Range(0, 4);
    }
}
