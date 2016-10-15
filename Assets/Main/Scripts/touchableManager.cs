using UnityEngine;
using System.Collections;

public class touchableManager : MonoBehaviour
{

	protected GameObject touchedObject;
	private RaycastHit hit;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	protected virtual void Update ()
	{
		if (Input.touchCount > 1) {
			
			if (Input.touches [0].phase == TouchPhase.Stationary) {
				Ray ray = Camera.main.ScreenPointToRay (Input.touches [0].position);
				if (Physics.Raycast (ray, out hit, 100.0f)) {
					touchedObject = hit.transform.gameObject; 
					touchedObject.GetComponent<touchableGameObject> ().Touched ();
					print (touchedObject.name);
				} else {
					print ("GetMouseButtonDown on nothing");
				}
			}
		}
	}
}