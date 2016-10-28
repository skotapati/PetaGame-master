using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour {
	public Vector2 firstPressPos;
	public Vector2 secondPressPos;
	public Vector2 currentSwipe;
	public swipes lastswipe;
	Animator animator;
	GameObject detectedObject;
	bool objectDetected;


	public enum swipes{up,down,left,right,tap,none};



	public swipes swipe(){
		if(Input.touches.Length > 0 ){
			Touch t = Input.GetTouch (0);
			if(t.phase == TouchPhase.Began&& CrossPlatformInputManager.GetButton("Swipe")){
				firstPressPos = new Vector2(t.position.x, t.position.y);
				animator.StartPlayback ();

				Ray ray = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
				RaycastHit hit;

				if ( Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.tag == "Enemy") {
						detectedObject = hit.collider.gameObject;
						detectedObject.GetComponent<EnemyController> ().onTap ();
						objectDetected = true;
						return swipes.none;
					}
					if (hit.collider.gameObject.tag == "Cage") {
						detectedObject = hit.collider.gameObject;
						detectedObject.GetComponent<AnimalCageController> ().onTap ();
						objectDetected = true;
						return swipes.none;
					}
				}
			}
			if(t.phase == TouchPhase.Ended){
				secondPressPos = new Vector2 (t.position.x, t.position.y);
				currentSwipe = new Vector2 (secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				currentSwipe.Normalize ();
				if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					lastswipe = swipes.up;
					return swipes.up;
				}if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
					lastswipe = swipes.down;
					animator.StartPlayback ();
					return swipes.down;
				}if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					lastswipe = swipes.up;
					return swipes.right;
					animator.StartPlayback ();
				}if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
					lastswipe = swipes.left;
					animator.StartPlayback ();
					return swipes.left;
				}
				//print ("tap");
				return swipes.tap;
				animator.StartPlayback ();
			}
			animator.StartPlayback ();
			return swipes.none;
		}
		animator.StartPlayback ();
		return swipes.none;
	}

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

	}
}
