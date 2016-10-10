using UnityEngine;
using System.Collections;

public class AlternatingSpikes : MonoBehaviour {
	public float startWait;
	public float timeInterval;
	Block block;
	// Use this for initialization
	void Start () {
		block = gameObject.GetComponent<Block> ();
		StartCoroutine (alternateSpikes ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator alternateSpikes(){
		bool i = true;
		while (i) {
			i = false;
			yield return new WaitForSeconds (startWait);
		}
		i = true;
		while (i) {
			block.walkable = !block.walkable;
			Renderer renderer = gameObject.GetComponent<Renderer> ();
			if (block.walkable) {
				renderer.material.color = Color.white;
			} 
			if (!block.walkable){
				renderer.material.color = Color.red;
			}
			yield return new WaitForSeconds (timeInterval);
		}
	}
}
