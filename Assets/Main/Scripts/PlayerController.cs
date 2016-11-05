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

	public int ammo = 3; //change later
	// Use this for initialization
	void Start () {
		gameController = gameObject.GetComponent<GameController> ();
		animator = gameObject.GetComponent<Animator>();
		tileRepo = GameObject.Find ("GameController").GetComponent<PositionRepo> ();
		Physics.gravity = new Vector3(0, -50.0F, 0);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, gameController.playerPosition.position, moveSpeed);
		playerBody.eulerAngles = Vector3.MoveTowards (playerBody.eulerAngles,new Vector3(0, gameController.playerPosition.rotation, 0),50f);
		Vector3 temp = new Vector3 (playerBody.position.x, 0, playerBody.position.z);
		if (temp == playerBody.position) {
			animator.StopPlayback ();
		}

		testBlock(); //dont need this now


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

	void checkScore(int current)
	{
		if (current >= PlayerPrefs.GetInt ("bestScore")) {
			print ("new highscore!"+current);
			PlayerPrefs.SetInt ("bestScore",current);
		}
	}

	void testBlock(){
		Block block = tileRepo.blockAtPosition (gameController.playerPosition.position);

		if (block.walkable == false) { 

			checkScore (GameController.score);
			//game over
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			GameController.gameOverClearVars ();
		}
	}

	//COLLISION - tried OnTriggerStay as well and OnCollisionStay, but score is incremented twice sometimes for both, need to fix
	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Enemy") {

			checkScore (GameController.score);

			//game over
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().name); /this needs to be uncommented for real
			//GameController.gameOverClearVars ();
			print ("game over by enemy");
		}
		if (col.gameObject.tag == "Animal") {
			print ("animal saved");
			Destroy (col.transform.gameObject);

			GameController.modifyScore (1); 

			GameController.increaseDifficulty (10);
			print ("score="+GameController.score);

		}
	}
}
