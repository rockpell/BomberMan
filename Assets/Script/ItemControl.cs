﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D :  " + other.gameObject.tag);
        if(other.tag == "Explosion") {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Destroyable") {
            
        }
    }

}