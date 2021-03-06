﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : Unit {

    [SerializeField]  private float speed = 1f;
    private Rigidbody2D rb;
    private GameObject coll_Obeject;

    // Use this for initialization
    void Start () {
        //life_point = 2;
        is_non_damage = false;

        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        randDirection();
    }
	
	// Update is called once per frame
	void Update () {
        if(life_point > 0) {
            move();
        } else {
            die();
        }
    }

    void randDirection() {
        direction = (unit_direction)Random.Range(0, 4);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        randDirection();
        coll_Obeject = coll.gameObject;
    }

    void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.Equals(coll_Obeject) && !(coll.gameObject.tag == "Player")) {
            randDirection();
        }

        if(coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<PlayerControl>().damaged();
        }
    }

    void move() {
        rb.AddForce(directionToVector2() * speed);
    }

    Vector2 directionToVector2() {
        Vector2 result = Vector2.zero;

        if(direction == unit_direction.down) {
            result = new Vector2(0, 1);
        } else if(direction == unit_direction.up) {
            result = new Vector2(0, -1);
        } else if(direction == unit_direction.right) {
            result = new Vector2(1, 0);
        } else if(direction == unit_direction.left) {
            result = new Vector2(-1, 0);
        }

        return result;
    }
}