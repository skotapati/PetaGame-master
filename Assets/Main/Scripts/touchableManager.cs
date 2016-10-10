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
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100.0f)) {
				touchedObject = hit.transform.gameObject; 
				touchedObject.GetComponent<touchableGameObject>().Touched();
			}
			else {
				print("GetMouseButtonDown on nothing");
			}
		}
	}
}