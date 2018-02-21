using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour {

    public Tilemap tilemap_destroyable;
    public Tilemap tilemap_immortal;

    private int explosion_level = 2;
    private int bomb_max_count = 2;
    private int bomb_now_count = 0;

    private int now_stage_level = 1;
    private int left_enemy = 9999;

    // Use this for initialization
    void Start () {
        refreshLeftEnmey();
    }
	
	// Update is called once per frame
	void Update () {

		if(left_enemy == 0)
        {
            nextStage();
        } else
        {
            refreshLeftEnmey();
        }
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

    public int getNowStageLevel() {
        return now_stage_level;
    }

    public void addStageLevel() {
        now_stage_level += 1;
    }

    public int getLeftEnemy() {
        return left_enemy;
    }

    public void refreshLeftEnmey() {
        left_enemy = GameObject.Find("Stage").transform.Find("Stage" + now_stage_level).transform.Find("Enemys").childCount;
    }

    public void reduceLeftEnmey() {
        left_enemy -= 1;
    }
    
    public void nextStage()
    {
        Debug.Log("now_stage_level : " + now_stage_level);
        GameObject.Find("Stage" + now_stage_level).SetActive(false);
        GameObject.Find("Stage").transform.Find("Stage" + (now_stage_level + 1)).gameObject.SetActive(true);

        addStageLevel();
        refreshLeftEnmey();
        movePositionPlayer();
        movePositionCamera();
    }

    private void movePositionPlayer()
    {
        Vector3 target_position = GameObject.Find("Stage" + now_stage_level).transform.Find("PlayerSpawn").transform.position;
        GameObject.Find("Player").transform.position = target_position;
    }

    private void movePositionCamera() {
        Vector3 temp = GameObject.Find("Main Camera").transform.position;
        GameObject.Find("Main Camera").transform.position = temp + new Vector3(25.8f, 0, 0);
    }
}
