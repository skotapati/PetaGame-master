using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionRepo : MonoBehaviour {
	public List<Tile> tiles;
	public Block air;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void addPosition(Vector3 p, Block b){
		tiles.Add (new Tile (p, b));
	}
	public Block blockAtPosition(Vector3 pos){
		for (int i = 0; i < tiles.Count; i++) {
			if (tiles [i].pos == pos) {
				return tiles [i].blockInfo;
			}
		}
		return air;
	}
}

[System.Serializable]
public class Tile{
	public Vector3 pos;
	public Block blockInfo;
	public Tile(Vector3 p, Block b){
		pos = p;
		blockInfo = b;
	}
}
