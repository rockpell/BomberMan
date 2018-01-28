using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExplosionControl : MonoBehaviour {
    private DataManager data_manager;
    private float left_time = 1f;

    // Use this for initialization
    void Start () {
        data_manager = GameObject.Find("DataMangerObject").GetComponent<DataManager>();
    }
	
	// Update is called once per frame
	void Update () {
        left_time -= Time.deltaTime;

        if(left_time <= 0) {
            Destroy(this.gameObject);
        }
    }

    void OnOnCollisionEnter2D(Collision2D coll) {
        //if(coll.gameObject.tag == "Destroyable") {
        //    Vector3 hitPosition = Vector3.zero;

        //    foreach(ContactPoint2D hit in coll.contacts) {
        //        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
        //        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
        //        //data_manager.setTileDestroyable(hitPosition, null);
        //    }
        //}
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Destroyable") {
            data_manager.setTileDestroyable(this.transform.position, null);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "Bomb") {
            other.gameObject.SendMessage("setBurst");
        }

        if(other.gameObject.tag == "Player") {
            Debug.Log("player is dead");
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //Debug.Log("OnTriggerStay2D : " + other.tag);
    }
}
