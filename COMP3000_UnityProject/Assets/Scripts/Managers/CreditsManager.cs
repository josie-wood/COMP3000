using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
	// Credits Manager deals with all behaviour to do with the Credits

	private SceneManagement sceneManagement;
	// Start is called before the first frame update
	void Start()
	{
		sceneManagement = GameObject.FindObjectOfType<SceneManagement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.anyKey)
		{
			sceneManagement.loadMenu();
		}
	}
}
