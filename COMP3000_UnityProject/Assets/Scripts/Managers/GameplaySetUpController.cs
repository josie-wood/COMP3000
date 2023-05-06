using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySetUpController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		SceneManagement sceneManagement = GameObject.FindObjectOfType<SceneManagement>();
        sceneManagement.loadGivenScene("4-GameplayScene");
	}
}
