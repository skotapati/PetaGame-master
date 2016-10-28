using UnityEngine;
using System.Collections;

public class AnimalCageController : MonoBehaviour {

	public enum AnimalType{Pig, Cow, Chicken};
	public AnimalType animalInside;

	public float maxSize = 2.0f;
	public float minSize = 0.2f;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}

	public void onTap(){
		print ("Remove the cage!"+ animalInside);
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		double range = maxSize - minSize;
		//transform.localScale.y = (Mathf.Sin(Time.time * speed) + 1.0) / 2.0 * range + minSize;
	
	}
}
