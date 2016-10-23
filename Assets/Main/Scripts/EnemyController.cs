using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float movementInterval;
	public Waypoint[] waypoints;
	public int currentPosition;
	public float speed;
	float lastMovement;
	float timeSinceLastMovement;

	public bool hit = false;
	public BallBehavior paintballPrefab1;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		currentPosition = 0;
		lastMovement = 0;
		timeSinceLastMovement = 0;

		player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		//player = GameObject.FindGameObjectsWithTag("PlayerBodyMatrix")[0]; //get player body
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastMovement = Time.timeSinceLevelLoad - lastMovement;
		if (hit == false) {
			if (timeSinceLastMovement >= movementInterval) {
				if (currentPosition == waypoints.Length - 1) {
					lastMovement = Time.timeSinceLevelLoad;
					currentPosition = 0;
				} else {
					lastMovement = Time.timeSinceLevelLoad;
					currentPosition++;
				}
			}
			transform.LookAt (waypoints [currentPosition].position.position);
			transform.position = Vector3.MoveTowards (transform.position, waypoints [currentPosition].position.position, speed);
		}
	}

	public void onTap(){
		print ("Curses, foiled again!");
		hit = true; 
		//Destroy (this.gameObject);
		if (player.ammo > 0) {
			
			print ("spawn paintball!");
			Vector3 playerPos = new Vector3 (player.transform.position.x -1.8f, player.transform.position.y+3.7f, player.transform.position.z+2.5f);
			BallBehavior newBall = Instantiate (paintballPrefab1, playerPos, Quaternion.identity) as BallBehavior;
	
			Vector3 ballPos = new Vector3(transform.position.x - 2, transform.position.y + 4, transform.position.z + 2);
			newBall.positionToGo = ballPos;
			//newBall.transform.position = Vector3.MoveTowards(newBall.transform.position, this.transform.position, speed*Time.deltaTime);
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Ball") {
			Destroy (this.gameObject);
			Destroy (col.gameObject);
		}
	}
		
}

[System.Serializable]
public class Waypoint{
	public Transform position;
}
