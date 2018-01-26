﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour {

    public enum player_direction {
        down = 0,
        left,
        up,
        right
    }

    public Animator animator;
    public player_direction direction = player_direction.down;
    public GameObject bomb;

    private DataManager data_manager;
    private bool is_on_bomb = false;
    private float speed = 4f;

    // Use this for initialization
    void Start () {
        data_manager = GameObject.Find("DataMangerObject").GetComponent<DataManager>();
    }
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Vertical"); //y
        float horizontal = Input.GetAxis("Horizontal"); //x

        animator.SetInteger("player_direction", (int)direction);

        if(vertical > 0) { // up
            direction = player_direction.up;
        } else if(vertical < 0) { //down
            direction = player_direction.down;
        }

        if(horizontal > 0) { // right
            direction = player_direction.right;
        } else if(horizontal < 0) { // left
            direction = player_direction.left;
        }

        animator.SetFloat("direction_x", horizontal);
        animator.SetFloat("direction_y", vertical);

        this.transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));

        if(Input.GetKeyDown(KeyCode.Space) && !is_on_bomb && data_manager.checkCreatableBomb()) {
            Instantiate(bomb, this.transform.position, Quaternion.identity);
            is_on_bomb = true;
            data_manager.addBombCount();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {

    }

    void setOutBomb() {
        is_on_bomb = false;
    }
}
