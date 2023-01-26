using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("2-MainMenuScene");
		Debug.Log("test debug");
	}

    public void loadSettings()
    {
        SceneManager.LoadScene("3-SettingsScene");
    }

    public void loadGameplay()
    {
        SceneManager.LoadScene("4-GameplayScene");
    }

    public void loadEnd()
    {
        SceneManager.LoadScene("5-EndScene");
    }

    public void closeApplication()
    {
        Debug.Log("Add close application on this btn");
    }
}
