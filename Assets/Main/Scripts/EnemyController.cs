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
	public GameObject playerBody;

	// Use this for initialization
	void Start () {
		currentPosition = 0;
		lastMovement = 0;
		timeSinceLastMovement = 0;

		player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		playerBody = GameObject.FindGameObjectsWithTag("PlayerBodyMatrix")[0];
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
		//Destroy (this.gameObject);
		if (player.ammo > 0 && hit == false) { //rotate player towards

			if (player.transform.position.z > this.transform.position.z) {
				//playerBody.transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

			}

			print ("spawn paintball!");

			Vector3 playerPos = new Vector3 (player.transform.position.x, player.transform.position.y+1.0f, player.transform.position.z);
			BallBehavior newBall = Instantiate (paintballPrefab1, playerPos, Quaternion.identity) as BallBehavior;

			Vector3 ballPos = new Vector3(transform.position.x, transform.position.y+2.0f, transform.position.z);
			newBall.positionToGo = ballPos;
		
		}
		hit = true;
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Ball") {
			Destroy (this.gameObject);

			GameController.modifyScore (10); 
			GameController.increaseDifficulty (20);

		}
	}
		
}

[System.Serializable]
public class Waypoint{
	public Transform position;
}
