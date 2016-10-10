using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public bool walkable;
	public bool moveable;

	public enum Hazards{Fire, Sharp, Squish, Fall, None};

	public Hazards hazard;
	void Start(){
		GameObject gameController = GameObject.Find ("GameController");
		PositionRepo repo = gameController.GetComponent<PositionRepo> ();
		repo.addPosition (transform.position,this);
	}
}
