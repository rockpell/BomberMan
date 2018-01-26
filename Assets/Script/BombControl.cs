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

        for(int i = 1; i < explosion_level + 1; i++) { // up
            Vector3 target_position2 = target_position + new Vector3(0, 1 * i, 0);
            if(data_manager.isImmortalTile(target_position2)) {
                break;
            } else if(is_check) {
                break;
            } else if(data_manager.isDestroyableTile(target_position2)) {
                is_check = true;
            }

            Instantiate(explosion, target_position2, Quaternion.identity);
        }

        is_check = false;

        for(int i = 1; i < explosion_level + 1; i++) { // down
            Vector3 target_position2 = target_position + new Vector3(0, -1 * i, 0);
            if(data_manager.isImmortalTile(target_position2)) {
                break;
            } else if(is_check) {
                break;
            } else if(data_manager.isDestroyableTile(target_position2)) {
                is_check = true;
            }

            Instantiate(explosion, target_position2, Quaternion.identity);
        }

        is_check = false;

        for(int i = 1; i < explosion_level + 1; i++) { // left
            Vector3 target_position2 = target_position + new Vector3(-1 * i, 0, 0);
            if(data_manager.isImmortalTile(target_position2)) {
                break;
            } else if(is_check) {
                break;
            } else if(data_manager.isDestroyableTile(target_position2)) {
                is_check = true;
            }

            Instantiate(explosion, target_position2, Quaternion.identity);
        }

        is_check = false;

        for(int i = 1; i < explosion_level + 1; i++) { // right
            Vector3 target_position2 = target_position + new Vector3(1 * i, 0, 0);
            if(data_manager.isImmortalTile(target_position2)) {
                break;
            } else if(is_check) {
                break;
            } else if(data_manager.isDestroyableTile(target_position2)) {
                is_check = true;
            }

            Instantiate(explosion, target_position2, Quaternion.identity);
        }
    }

    void setBurst() {
        explosion_left_time = 0;
    }
}
