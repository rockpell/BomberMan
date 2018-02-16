using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject game_over_UI;
    public GameObject game_clear_UI;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activeGameOverUI(bool value) {
        game_over_UI.SetActive(value);
    }

    public void activeGameClearUI(bool value) {
        game_clear_UI.SetActive(value);
    }
}
