using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour {

    public Tilemap tilemap_destroyable;
    public Tilemap tilemap_immortal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTileDestroyable(Vector3 position, TileBase tile) {
        tilemap_destroyable.SetTile(tilemap_destroyable.WorldToCell(position), null);
    }

    public bool isDestroyableTile(Vector3 position) {
        return tilemap_destroyable.HasTile(tilemap_destroyable.WorldToCell(position));
    }

    public bool isImmortalTile(Vector3 position) {
        return tilemap_immortal.HasTile(tilemap_destroyable.WorldToCell(position));
    }
}
