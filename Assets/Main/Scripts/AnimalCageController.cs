using UnityEngine;
using System.Collections;

public class AnimalCageController : MonoBehaviour {

	public enum AnimalType{Pig, Cow, Chicken};
	public AnimalType animalInside;


	// Use this for initialization
	void Start () {
		//touchAnim ();
	
	}

	void touchAnim( )
	{
		//StartCoroutine(waitForGrow());
		gameObject.GetComponent<Animation>().Play ();
		waitForGrow ();
	}

	public void onTap(){
		print ("Remove the cage!"+ animalInside);
		touchAnim ();
		//Destroy (this.gameObject);
	}

	IEnumerator waitForGrow() {
		
		yield return new WaitForSeconds (25);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
