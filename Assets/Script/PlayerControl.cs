using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : Unit {

    public Animator animator;
    public GameObject bomb;

    private DataManager data_manager;
    private bool is_on_bomb = false;
    private float speed = 4f;

    // Use this for initialization
    void Start () {
        life_point = 2;
        is_non_damage = false;
        direction = unit_direction.down;
        spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        data_manager = GameObject.Find("DataMangerObject").GetComponent<DataManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space) && !is_on_bomb && data_manager.checkCreatableBomb()) {
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
        float vertical = Input.GetAxis("Vertical"); //y
        float horizontal = Input.GetAxis("Horizontal"); //x

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

        this.transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));
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

    void setOutBomb() {
        is_on_bomb = false;
    }

    new private void die() {
        // player die
    }
}
