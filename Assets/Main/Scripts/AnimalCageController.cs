using UnityEngine;
using System.Collections;

public class AnimalCageController : MonoBehaviour {

	public enum AnimalType{Pig, Cow, Chicken, Turkey};
	public AnimalType animalInside;
	private GameObject animalToSpawn;
	public Vector3 cagePos;

	public bool wasMoved = false;

	//obj to be spawned
	public GameObject pigObj;
	public GameObject cowObj;
	public GameObject chickenObj;
	public GameObject turkeyObj;


	// Use this for initialization
	void Start () {
		
		switch (animalInside) { //modify vectors based on animal spawned
		case AnimalType.Pig:
			animalToSpawn = pigObj;
			cagePos = new Vector3 (gameObject.transform.position.x+2.2f, gameObject.transform.position.y+2.9f, gameObject.transform.position.z-1.7f);
			break;
		case AnimalType.Cow:
			animalToSpawn = cowObj;
			cagePos = new Vector3 (gameObject.transform.position.x-1.6f, gameObject.transform.position.y-1.0f, gameObject.transform.position.z+1.1f);
			break;
		case AnimalType.Chicken:
			animalToSpawn = chickenObj;
			cagePos = new Vector3 (gameObject.transform.position.x-2.2f, gameObject.transform.position.y-0.32f, gameObject.transform.position.z+1.5f); //this is right
			break;
		case AnimalType.Turkey:
			animalToSpawn = turkeyObj;
			cagePos = new Vector3 (gameObject.transform.position.x-2.0f, gameObject.transform.position.y-0.26f, gameObject.transform.position.z+1.5f); //this is right
			break;
		default:
			animalToSpawn = pigObj;
			cagePos = new Vector3 (gameObject.transform.position.x-0.5f, gameObject.transform.position.y-0.25f, gameObject.transform.position.z-2.9f);
			break;
		}
	
	}

	void touchAnim( )
	{

		gameObject.GetComponent<Animation>().Play ();

	}
	void spawnAnimal()
	{

		Destroy (this.gameObject);
		//Vector3 cagePos = new Vector3 (gameObject.transform.position.x-0.5f, gameObject.transform.position.y-0.25f, gameObject.transform.position.z-2.9f);
		if (wasMoved == false) {
			AnimalController animal = Instantiate (animalToSpawn, cagePos, gameObject.transform.localRotation) as AnimalController;
		}

//		rb = animal.GetComponent<Rigidbody>();
//		rb.AddForce(2,4, 0, ForceMode.Impulse);
	}

	public void onTap(){
		print ("Remove the cage!"+ animalInside);
		//touchAnim ();
		StartCoroutine (waitForGrow ());

	}

	IEnumerator waitForGrow() {

		touchAnim ();
		yield return new WaitForSeconds (0.7f);
		spawnAnimal ();

	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
