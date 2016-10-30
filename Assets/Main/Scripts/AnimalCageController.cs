using UnityEngine;
using System.Collections;

public class AnimalCageController : MonoBehaviour {

	public enum AnimalType{Pig, Cow, Chicken};
	public AnimalType animalInside;
	private GameObject animalToSpawn;
	public Vector3 cagePos;

	//obj to be spawned
	public GameObject pigObj;
	public GameObject cowObj;
	public GameObject chickenObj;


	// Use this for initialization
	void Start () {
		switch (animalInside) { //modify vectors based on animal spawned
		case AnimalType.Pig:
			animalToSpawn = pigObj;
			cagePos = new Vector3 (gameObject.transform.position.x-0.5f, gameObject.transform.position.y-0.25f, gameObject.transform.position.z);
			break;
		case AnimalType.Cow:
			animalToSpawn = cowObj;
			cagePos = new Vector3 (gameObject.transform.position.x-0.5f, gameObject.transform.position.y-0.25f, gameObject.transform.position.z);
			break;
		case AnimalType.Chicken:
			animalToSpawn = chickenObj;
			cagePos = new Vector3 (gameObject.transform.position.x-2.0f, gameObject.transform.position.y-0.26f, gameObject.transform.position.z+1.0f); //this is right
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
		AnimalController animal = Instantiate (animalToSpawn, cagePos, gameObject.transform.localRotation) as AnimalController;

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
