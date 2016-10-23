using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

	public float speed;
	public Vector3 positionToGo;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards(transform.position, positionToGo, speed*Time.deltaTime);
	}
}
