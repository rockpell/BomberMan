using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public enum player_direction {
        down = 0,
        left,
        up,
        right
    }

    public Animator animator;
    public player_direction state = player_direction.down;

    private float speed = 4f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float vertical = Input.GetAxis("Vertical"); //y
        float horizontal = Input.GetAxis("Horizontal"); //x

        animator.SetInteger("player_direction", (int)state);

        if(vertical > 0) { // up
            state = player_direction.up;
        } else if(vertical < 0) { //down
            state = player_direction.down;
        }

        if(horizontal > 0) { // right
            state = player_direction.right;
        } else if(horizontal < 0) { // left
            state = player_direction.left;
        }

        animator.SetFloat("direction_x", horizontal);
        animator.SetFloat("direction_y", vertical);

        this.transform.Translate(new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0));
    }
}
