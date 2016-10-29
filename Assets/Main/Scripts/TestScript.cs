using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {


	public float thrust;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		StartCoroutine(waitForJump ());
	}

	IEnumerator waitForJump() {

		yield return new WaitForSeconds (3);
		rb.AddForce(2,10, 0, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		
		//rb.AddForce(transform.forward * 200,ForceMode.Impulse);
	}
}
