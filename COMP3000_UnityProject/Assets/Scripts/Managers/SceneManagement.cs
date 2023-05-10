using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public Transform playerSpawnLocation;
	public GameObject player;
    public YarnManager yarnManager;

    // Start is called before the first frame update
    void Start()
    {
        yarnManager = FindObjectOfType<YarnManager>();
        if (yarnManager)
        {
            yarnManager.OnSceneLoaded();
        }

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

		if (playerSpawnLocation && player)
        {
            player.transform.position = playerSpawnLocation.position;
            player.GetComponent<PlayerController>().unlockMovement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGivenScene(string sceneName)
    {
        //loads scene based on string given
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch 
        {
            Debug.Log("couldnt load a scene called: " + sceneName);
        }
	}
    public void loadMenu()
    {
        Destroy(player);
        Destroy(yarnManager);
        SceneManager.LoadScene("2-MainMenuScene");
		Debug.Log("test debug");
	}

    public void loadSettings()
    {
        Destroy(player);
		Destroy(yarnManager);
		SceneManager.LoadScene("3-SettingsScene");
	}

	public void loadGameplay()
    {
        SceneManager.LoadScene("4.2-GameplaySetUp");
    }

    public void loadEnd()
    {
		Destroy(player);
		Destroy(yarnManager);
		SceneManager.LoadScene("5-EndScene");
	}

	public void closeApplication()
    {
		Destroy(player);
		Destroy(yarnManager);
		Application.Quit();
    }
}
