using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerSelector : MonoBehaviour {
	
	Canvas canvas;
	bool beforeProjection = false;
	float tmpNearClipPlane = -1;
	
	void Start(){
		canvas = gameObject.GetComponent<Canvas>();
		canvas.enabled = false;
		transform.FindChild ("Panel").gameObject.SetActive (false);

	}

	public void clickCanvas(){
		canvas.enabled = !canvas.enabled;
		transform.FindChild ("Panel").gameObject.SetActive (canvas.enabled);

		//Change the render mode if needed
		if(canvas.enabled && Camera.main.orthographic == false){
			Camera.main.orthographic = true;
			beforeProjection = true;
		}
		if(beforeProjection && canvas.enabled == false){
			Camera.main.orthographic = false;
			beforeProjection = false;
		}


		//Store and restore the near plane
		if(canvas.enabled && Camera.main.nearClipPlane > -200){
			tmpNearClipPlane = Camera.main.nearClipPlane;
			Camera.main.nearClipPlane = -200;
		}
		if(canvas.enabled == false && tmpNearClipPlane != -1){
			Camera.main.nearClipPlane = tmpNearClipPlane;
		}
	}
}