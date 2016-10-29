using UnityEngine;
using System.Collections;

public class AnimalCageController : MonoBehaviour {

	public enum AnimalType{Pig, Cow, Chicken};
	public AnimalType animalInside;
	private AnimalController animalToSpawn;

	public float thrust;
	public Rigidbody rb;

	//obj to be spawned
	public AnimalController pigObj;
	public AnimalController cowObj;
	public AnimalController chickenObj;


	// Use this for initialization
	void Start () {
		switch (animalInside) {
		case AnimalType.Pig:
			animalToSpawn = pigObj;
			break;
		case AnimalType.Cow:
			animalToSpawn = cowObj;
			break;
		case AnimalType.Chicken:
			animalToSpawn = chickenObj;
			break;
		default:
			animalToSpawn = pigObj;
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
		Vector3 cagePos = new Vector3 (gameObject.transform.position.x-1.8f, gameObject.transform.position.y+0.4f, gameObject.transform.position.z-0.3f);
		AnimalController animal = Instantiate (animalToSpawn, cagePos, transform.rotation) as AnimalController;

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
