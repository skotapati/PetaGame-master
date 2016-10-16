using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class touchableGameObject : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	}
		

	void Update ()
	{ 
	}

	public void Touched()
	{
		print ("touched!! WORKING");
		Destroy (this.gameObject);
	}
}