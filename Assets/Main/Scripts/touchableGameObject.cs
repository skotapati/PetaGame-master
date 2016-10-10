using UnityEngine;
using System.Collections;

public class touchableGameObject : MonoBehaviour
{

	GameObject sprite;

	// Use this for initialization
	void Start ()
	{
		sprite = GetComponent<GameObject>();
		print ("start mousemanager for "+sprite.name);
	}

	void OnMouseDown ()
	{
		print("Received a mouse down on "+sprite.name);
	}

	void Update ()
	{ 
	}

	public void Touched()
	{
		print ("touched!!");
	}
}