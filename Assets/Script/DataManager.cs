using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour {

    public Tilemap tilemap_destroyable;
    public Tilemap tilemap_immortal;

    private int explosion_level = 2;
    private int bomb_max_count = 1;
    private int bomb_now_count = 0;

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
    
    public void addExplosionLevel() {
        explosion_level += 1;
    }

    public int getExplosionLevel() {
        return explosion_level;
    }

    public int getBombNowCount() {
        return bomb_now_count;
    }

    public int getBombMaxCount() {
        return bomb_max_count;
    }

    public void addBombCount() {
        if(bomb_max_count - bomb_now_count > 0) {
            bomb_now_count += 1;
        }
        
    }

    public void reduceBombCount() {
        if(bomb_now_count > 0) {
            bomb_now_count -= 1;
        }
    }

    public void addBombMaxCount() {
        bomb_max_count += 1;
    }

    public bool checkCreatableBomb() {
        if(bomb_max_count - bomb_now_count > 0) {
            return true;
        }
        return false;
    }
}
