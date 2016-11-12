using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour {

	public RectTransform panel;
	public RectTransform center;
	public GameObject[] prefab;

	public Text txtName;

	float[] distance;

	bool dragging = false;

	int minButtonNum;
	int currentSelectedPly = -1;

	public float objectScale = 1.7f;
	public int bttnDistance = 300;

	void OnEnable() {
		//txtGeneralCash.text = "" + PlayerPrefs.GetInt ("money", 0);
	}

	void Start(){
		distance = new float[prefab.Length];

		//instatiate the prefab
		for(int i=0; i<prefab.Length;i++){
			prefab[i] =  Instantiate(prefab[i],center.transform.position,Camera.main.transform.rotation) as GameObject;
			prefab [i].transform.SetParent(panel.transform);
			Vector3 pos = prefab[i].GetComponent<RectTransform>().anchoredPosition;
			pos.x += (i * bttnDistance);
			prefab [i].GetComponent<RectTransform> ().anchoredPosition = pos; 
		}
	}

	void Update(){
		//calculate the relative distance
		for(int i=0;i<prefab.Length;i++){
			distance [i] = Mathf.Abs (center.transform.position.x - prefab [i].transform.position.x);
		}

		float minDistance = Mathf.Min (distance);

		// Aplly the scale to object
		for(int a=0;a<prefab.Length;a++){
			if (minDistance == distance [a]) {
				minButtonNum = a;
				if(minButtonNum != currentSelectedPly){
					//lookAtPrice (minButtonNum);
					scaleButtonCenter (minButtonNum);
					currentSelectedPly = minButtonNum;
					txtName.text = prefab [minButtonNum].GetComponent<CharacterProperty> ().nameObj;
				}
			}
		}
			
		// if the users aren't dragging the lerp function is called on the prefab
		if(!dragging){
			LerpToBttn (currentSelectedPly* (-bttnDistance));
		}
			
	}

	/*
	 *  Lerp the nearest prefab to center 
	 */
	void LerpToBttn(int position){
		float newX = Mathf.Lerp (panel.anchoredPosition.x,position,Time.deltaTime*7f);
		Vector2 newPosition = new Vector2 (newX,panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
	}

	/*
	 * Set the scale of the prefab on center to 2, other to 1
	 */
	public void scaleButtonCenter (int minButtonNum){
		for (int a = 0; a < prefab.Length; a++) {
			if (a == minButtonNum) {
				StartCoroutine (ScaleTransform(prefab [a].transform,prefab [a].transform.localScale,new Vector3 (objectScale,objectScale,objectScale)));
			} else {
				StartCoroutine (ScaleTransform(prefab [a].transform,prefab [a].transform.localScale,new Vector3 (1f, 1f, 1f)));
			}
		}
	}

	/*
	 * If the prefab is not free, show the price button
	 */
//	public void lookAtPrice (int minButtonNum){
//		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
//		if (chrProperty.price == 0 || PlayerPrefs.GetInt(chrProperty.name,-1) == 7) {
//			btnFree.SetActive (true);
//			btnPrice.SetActive (false);
//		} else {
//			btnFree.SetActive (false);
//			btnPrice.SetActive (true);
//			txtPriceCash.text = "" + ((int)CharacterProperty.CONVERSION_RATE*chrProperty.price);
//			txtPriceSold.text = chrProperty.price + " €";
//		}
//	}

	/*
	 * Courutine for change the scale
	 */
	IEnumerator ScaleTransform(Transform transformTrg,Vector3 initScale,Vector3 endScale){
		float completeTime = 0.2f;//How much time will it take to scale
		float currentTime = 0.0f;
		bool done = false;

		while (!done){
			float percent = currentTime / completeTime;
			if (percent >= 1.0f){
				percent = 1;
				done = true;
			}
			transformTrg.localScale = Vector3.Lerp(initScale, endScale, percent);
			currentTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	/*
	 * Called by the canvas, set dragging to true for preventing lerp when users are dragging
	 */
	public void StartDrag(){
		dragging = true;
	}

	/*
	 * Called by the canvas, set dragging to true for preventing lerp when users are dragging
	 */
	public void EndDrag(){
		dragging = false;
	}

	/*
	 * Called when character is selected, it change the player model
	 */
	public void CharacterSelected(){
		bool oneEnable = false;
		string nameSelected = prefab [currentSelectedPly].GetComponent<CharacterProperty> ().name;
		nameSelected = nameSelected.Split('(')[0];
		GameObject player = GameObject.Find ("CharactersPlayer");
		if(player != null){
			foreach (Transform child in player.transform) {
				if (child.gameObject.name == nameSelected) {
					child.gameObject.SetActive (true);
					oneEnable = true;
					PlayerPrefs.SetString ("SelectedPlayer", nameSelected);
				} else {
					child.gameObject.SetActive (false);
				}
			}

			// if no one was selected
			if (oneEnable == false) {
				player.transform.GetChild (0).gameObject.SetActive (true);
			}
		}
	}

	public void chooseCharacter()
	{
		var charInt = prefab [currentSelectedPly].GetComponent<CharacterProperty> ().idNum;
		PlayerPrefs.SetInt ("player", charInt);

		print ("new player selected"+PlayerPrefs.GetInt("player"));
	}

	/*
	 * Called when player try to buy character with cash
	 */
//	public void buyCharacterWithCash(){
//		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
//		int cashNedeed = (int)(CharacterProperty.CONVERSION_RATE*chrProperty.price);
//		int totalCash = int.Parse (txtGeneralCash.text);
//		if (cashNedeed <= totalCash) {
//			totalCash -= cashNedeed;
//			txtGeneralCash.text = "" + totalCash;
//
//			btnFree.SetActive (true);
//			btnPrice.SetActive (false);
//
//			PlayerPrefs.SetInt (chrProperty.name, 7);
//			PlayerPrefs.SetInt ("money", totalCash);
//		} else {
//			txtGeneralCash.color = new Color (255,0,0);
//		}
//	}

	/*
	 * Implement here your pay function
	 */
//	public void buyCharacterWithPayment(){
//		CharacterProperty chrProperty = prefab [minButtonNum].GetComponent<CharacterProperty> ();
//		float price = chrProperty.price;
//
//		/*
//		 * Here player payment 
//		 */
//
//		Debug.Log ("You must pay ! ;) Price: " + price + "€");
//
//		//<--- Call this when player confirm payment !
//
//		/*PlayerPrefs.SetInt (chrProperty.name, 7);
//		btnFree.SetActive (true);
//		btnPrice.SetActive (false);*/
//	}
}
