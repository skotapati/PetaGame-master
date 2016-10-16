using UnityEngine;
using System.Collections;

public class AnimalController : MonoBehaviour {

	public float movementInterval; 
	public int currentPosition;
	public float speed;
	float lastMovement;
	float timeSinceLastMovement;

	// Use this for initialization
	void Start () {

		currentPosition = 0;
		lastMovement = 0;
		timeSinceLastMovement = 0;
	
	}

//	void OnTriggerEnter(Collider col)
//	{
//		if (col.gameObject.tag == "Player") {
//			print ("PLAYER!");
//		}
//	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastMovement = Time.timeSinceLevelLoad - lastMovement;
		//recurring action =
	}
}
