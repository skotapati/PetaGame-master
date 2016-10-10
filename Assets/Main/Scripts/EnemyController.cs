using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public float movementInterval;
	public Waypoint[] waypoints;
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
	
	// Update is called once per frame
	void Update () {
		timeSinceLastMovement = Time.timeSinceLevelLoad - lastMovement;
		if (timeSinceLastMovement >= movementInterval) {
			if (currentPosition == waypoints.Length-1) {
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

[System.Serializable]
public class Waypoint{
	public Transform position;
}
