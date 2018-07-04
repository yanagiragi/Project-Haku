using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MHHScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MHHFailedScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ClearOpening");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("OneMoreChanceScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ChurchFailedScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ChurchGoodEnding");
        }
    }
}
