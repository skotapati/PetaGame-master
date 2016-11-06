using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public Position playerPosition;
	public Position previousPosition; //If the player makes an invalid move, the character will snap back to previous position
	public float previousRotation = 0; // so they can turn from the previous rot
	public Position gamePosition;

	public GameObject[] sectorsZone1;
	public GameObject[] beginSectorsZone1;

	public bool losing;

	//NOTE: these statics have to be reset explicitly because they dont die with scene
	public static int score;
	public static int difficulty = 10; //just a starting number

	public int numSectorsToSpawn = 3; //just far enough that they cant see

	public static int x
	{
		get { return x; }
		set {x = value;}
	}

	InputManager input;

	// Use this for initialization
	void Start () {
		losing = false;
		score = 0;

		input = gameObject.GetComponent<InputManager> ();

		//initial spawn
		int i = 10; //get position of last sectors, when its exceeded by player then delete and spawn again
		//print(numSectorsToSpawn+"duh2");

		while (i >= 0) {

			if (i >= 9) {

				int j = Random.Range (0, beginSectorsZone1.Length);
				while (beginSectorsZone1 [j].GetComponent<Sector> ().weight >= difficulty) { //if greater than difficulty cant be spawned
					j = Random.Range (0, beginSectorsZone1.Length);
				}
				spawnSector (gamePosition.position, beginSectorsZone1 [j]);
			} else {
				int j = Random.Range (0, sectorsZone1.Length);
				while (sectorsZone1 [j].GetComponent<Sector> ().weight >= difficulty) { //if greater than difficulty cant be spawned
					j = Random.Range (0, sectorsZone1.Length);
				}
				spawnSector (gamePosition.position, sectorsZone1 [j]);
			}
			i--;
		}

	}

	public static void increaseDifficulty(int increase)
	{
		difficulty += increase;
		print ("difficulty="+GameController.difficulty);
	}
	public static void modifyScore(int num)
	{
		score += num;
	}

	void spawnSectorGroup()
	{
		int i = numSectorsToSpawn; //get position of last sectors, when its exceeded by player then delete and spawn again
		while (i >= 0) {
			int j = Random.Range (0, sectorsZone1.Length);
			while (sectorsZone1 [j].GetComponent<Sector> ().weight >= difficulty) { //if greater than difficulty cant be spawned
				j = Random.Range (0, sectorsZone1.Length);
			}
			spawnSector (gamePosition.position, sectorsZone1 [j]);
			i--;
		}
	}
	public static void gameOverClearVars()
	{
		score = 0;
		difficulty = 10;
	}

	void checkAndUpdateSectors() 
	{

		//could increase difficulty here too
		if (gamePosition.position.z - playerPosition.position.z <= 20) { //safe value ahead
			print ("theyre almost there, spawn new group");
			print (gamePosition.position.z - playerPosition.position.z);
			spawnSectorGroup ();

		}

		//destroy past ones
		GameObject[] objects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		//GameObject[] objects = GameObject.FindGameObjectsWithTag("Block");
		foreach (GameObject o in objects) {

			if (o.gameObject.tag == "Block" || o.gameObject.tag == "Cage" || o.gameObject.tag == "Animal" || o.gameObject.tag == "Enemy") {
				
				if (playerPosition.position.z - o.transform.position.z >= 100) { //safe aways back

					print ("destroying past object");
					print (o.transform.position);
					Destroy (o.gameObject);
				}
			}

		}

	}
	
	// Update is called once per frame
	void Update () {


		//Take #3
		InputManager.swipes swipeDirection = InputManager.swipes.none;
		swipeDirection = input.swipe ();
		if (swipeDirection == InputManager.swipes.down) {
			previousPosition.position = playerPosition.position;
			//playerPosition.position = playerPosition.Behind ();
			playerPosition.rotation = 180;
			//print ("swiped down");
		} else if (swipeDirection == InputManager.swipes.up) {
			previousPosition.position = playerPosition.position;
			//playerPosition.position = playerPosition.Forward ();
			playerPosition.rotation = 0;
			//print("swiped up- jump");
		} else if (swipeDirection == InputManager.swipes.right) {
			previousPosition.position = playerPosition.position;
			//playerPosition.position = playerPosition.Right ();
			playerPosition.rotation = 90;

		} else if (swipeDirection == InputManager.swipes.left) {
			previousPosition.position = playerPosition.position;

			//playerPosition.position = playerPosition.Left ();
			playerPosition.rotation = 270;
		}
		if (swipeDirection == InputManager.swipes.tap) {
			checkAndUpdateSectors ();

			if (playerPosition.rotation == 0) {
				previousPosition.position = playerPosition.position;
				playerPosition.position = playerPosition.Forward ();
			} else if (playerPosition.rotation == 90) {
				previousPosition.position = playerPosition.position;
				playerPosition.position = playerPosition.Right ();
			} else if (playerPosition.rotation == 180) {
				previousPosition.position = playerPosition.position;
				playerPosition.position = playerPosition.Behind ();
			} else if (playerPosition.rotation == 270) {
				previousPosition.position = playerPosition.position;
				playerPosition.position = playerPosition.Left ();
			}
		}
			
	}

	void spawnSector(Vector3 relativePosition,GameObject sector){
		GameObject newSector = GameObject.Instantiate (sector, relativePosition, transform.rotation) as GameObject;
		gamePosition.position += newSector.GetComponent<Sector> ().length;
	}
		
}

[System.Serializable]
public class Position{
	public Vector3 position;
	public float rotation;
	public Position(float x, float y, float z){
		position.x = x - (x % 1);
		position.y = y - (y % 1);
		position.z = z - (z % 1);
	}
	public Vector3 Above(){
		return new Vector3 (position.x, position.y + 1, position.z);
	}
	public Vector3 Below(){
		return new Vector3 (position.x, position.y - 1, position.z);
	}
	public Vector3 Right(){
		return new Vector3 (position.x + 1, position.y, position.z);
	}
	public Vector3 Left(){
		return new Vector3 (position.x - 1, position.y, position.z);
	}
	public Vector3 Forward(){
		return new Vector3 (position.x, position.y, position.z + 1);
	}
	public Vector3 Behind(){
		return new Vector3 (position.x, position.y, position.z - 1);
	}
	public void getBlockAtPosition(Position pos){
		
	}
}