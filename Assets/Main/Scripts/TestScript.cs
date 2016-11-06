using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {


	public GameObject pieces;

	// Use this for initialization
	void Start () {

	}
		
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.Space))
		{
			Instantiate(pieces, gameObject.transform.localPosition, gameObject.transform.rotation);

			Destroy(this.gameObject);
		}
	}
}
