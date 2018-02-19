﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : Unit {

    public Animator animator;
    public GameObject bomb;

    private Rigidbody2D rb;
    private DataManager data_manager;
    private UIManager ui_manager;
    private bool is_on_bomb = false;
    private bool is_dead = false;
    [SerializeField] private float speed = 200f;
    

    // Use this for initialization
    void Start () {
        //life_point = 2;
        is_non_damage = false;
        direction = unit_direction.down;
        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        data_manager = GameObject.Find("MangerObject").GetComponent<DataManager>();
        ui_manager = GameObject.Find("MangerObject").GetComponent<UIManager>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if(!is_dead && Input.GetKeyDown(KeyCode.Space) && !is_on_bomb && data_manager.checkCreatableBomb()) {
            Instantiate(bomb, this.transform.position, Quaternion.identity);
            is_on_bomb = true;
            data_manager.addBombCount();
        }

        if(life_point > 0) {

        } else {
            die();
        }
    }

    void FixedUpdate() {
        float vertical = 0;
        float horizontal = 0;

        if (!is_dead) {
            vertical = Input.GetAxis("Vertical"); //y
            horizontal = Input.GetAxis("Horizontal"); //x
        }

        rb.velocity = Vector3.zero;

        animator.SetInteger("player_direction", (int)direction);

        if(vertical > 0) { // up
            direction = unit_direction.up;
        } else if(vertical < 0) { //down
            direction = unit_direction.down;
        }

        if(horizontal > 0) { // right
            direction = unit_direction.right;
        } else if(horizontal < 0) { // left
            direction = unit_direction.left;
        }

        animator.SetFloat("direction_x", horizontal);
        animator.SetFloat("direction_y", vertical);

        rb.AddForce(new Vector3(horizontal, vertical, 0) * speed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "bomb_plus") {
            data_manager.addBombMaxCount();
            Destroy(other.gameObject);
        } else if(other.gameObject.tag == "explosion_plus") {
            data_manager.addExplosionLevel();
            Destroy(other.gameObject);
        }
    }

    public void setOutBomb() {
        is_on_bomb = false;
    }

    new private void die() {
        // player die
        is_dead = true;
        ui_manager.activeGameOverUI(true);
        //this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
