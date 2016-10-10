using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public GameController gameController;
	public float moveSpeed;
	public float direction;
	public Transform raycastPosition;
	public Transform playerBody;
	public float playerHeight;
	PositionRepo tileRepo;
	Animator animator;
	// Use this for initialization
	void Start () {
		gameController = gameObject.GetComponent<GameController> ();
		animator = gameObject.GetComponent<Animator>();
		tileRepo = GameObject.Find ("GameController").GetComponent<PositionRepo> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, gameController.playerPosition.position, moveSpeed);
		playerBody.eulerAngles = Vector3.MoveTowards (playerBody.eulerAngles,new Vector3(0, gameController.playerPosition.rotation, 0),50f);
		Vector3 temp = new Vector3 (playerBody.position.x, 0, playerBody.position.z);
		if (temp == playerBody.position) {
			animator.StopPlayback ();
		}
		//checkBlock ();

		testBlock();
		playerBody.position= new Vector3(playerBody.position.x, playerHeight, playerBody.position.z);
	}
	void checkBlock(){
		RaycastHit hit;
		if (Physics.Raycast (raycastPosition.position, -Vector3.up, out hit, 100.0f)) {
			if (hit.collider.gameObject.tag == "Block") {
				Block block = hit.collider.gameObject.GetComponent<Block> ();
				if (block.walkable) {
						
				} else {
					Debug.Log ("Level End V1");
					SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				}
			} else {
				Debug.Log ("Level end V2");
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			Debug.Log (hit.collider.gameObject.name);
		} else {
			Debug.Log ("Level end V3");
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	void testBlock(){
		Block block = tileRepo.blockAtPosition (gameController.playerPosition.position);
		if (block.walkable == false) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy") {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
