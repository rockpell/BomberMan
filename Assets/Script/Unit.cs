using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    protected enum unit_direction {
        down = 0,
        left,
        up,
        right
    }
    protected unit_direction direction;
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected int life_point;
    protected bool is_non_damage = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected IEnumerator nonDamageTime() {
        int count_time = 0;

        while(count_time < 10) {
            if(count_time % 2 == 0) {
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            } else {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }

            yield return new WaitForSeconds(0.2f);

            count_time += 1;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        is_non_damage = false;

        yield return null;
    }

    protected IEnumerator slowDiedisappear() {
        byte invisible_value = 255;

        while(invisible_value > 1) {
            invisible_value -= 2;
            spriteRenderer.color = new Color32(255, 255, 255, invisible_value);

            yield return new WaitForSeconds(0.006f);
        }
        Destroy(this.gameObject);
        yield return null;
    }

    protected void die() {
        StartCoroutine("slowDiedisappear");
    }

    public void damaged() {

        if(!is_non_damage) {
            life_point -= 1;
            is_non_damage = true;
            StartCoroutine("nonDamageTime");
        }

    }
}
