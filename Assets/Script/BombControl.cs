using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour {

    public GameObject explosion;

    private BoxCollider2D box_coll;
    private DataManager data_manager;

    private float explosion_left_time = 3f;
    private int explosion_level;

    // Use this for initialization
    void Start () {
        box_coll = GetComponent<BoxCollider2D>();
        data_manager = GameObject.Find("DataMangerObject").GetComponent<DataManager>();
        explosion_level = data_manager.getExplosionLevel();
    }
	
	// Update is called once per frame
	void Update () {
        explosion_left_time -= Time.deltaTime;

        if(explosion_left_time <= 0) {
            checkExplosion();
            destroyer();
        }
	}

    void destroyer() {
        data_manager.reduceBombCount();
        Destroy(this.gameObject);
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            box_coll.isTrigger = false;
            other.gameObject.SendMessage("setOutBomb");
        }
    }

    void checkExplosion() {
        Vector3 target_position = this.gameObject.transform.position;
        Instantiate(explosion, target_position, Quaternion.identity);

        bool is_check = false;

        for(int p =0; p < 4; p++) {
            for(int i = 1; i < explosion_level + 1; i++) { // up
                Vector3 target_position2 = Vector3.zero;
                if(p == 0) {
                    target_position2 = target_position + new Vector3(0, 1 * i, 0);
                } else if(p == 1) {
                    target_position2 = target_position + new Vector3(0, -1 * i, 0);
                } else if(p == 2) {
                    target_position2 = target_position + new Vector3(-1 * i, 0, 0);
                } else if(p == 3) {
                    target_position2 = target_position + new Vector3(1 * i, 0, 0);
                }

                if(data_manager.isImmortalTile(target_position2)) {
                    break;
                } else if(is_check) {
                    break;
                } else if(castRayBomb(target_position2)) {
                    is_check = true;
                } else if(data_manager.isDestroyableTile(target_position2)) {
                    data_manager.setTileDestroyable(target_position2, null);
                    break;
                }

                Instantiate(explosion, target_position2, Quaternion.identity);
            }
        }
    }

    void setBurst() {
        explosion_left_time = 0;
    }

    bool castRayBomb(Vector2 target_position) {
        RaycastHit2D hit = Physics2D.Raycast(target_position, Vector2.zero, 0f);

        if(hit.collider != null) {
            //Debug.Log(target_position + " cast ray : " + hit.collider.gameObject.tag);
            if(hit.collider.gameObject.tag == "Bomb") {
                return true;
            }
        }

        return false;
    }
}
