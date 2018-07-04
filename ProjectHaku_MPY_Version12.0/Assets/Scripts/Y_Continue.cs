using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Continue : MonoBehaviour {
    public bool isSpecialCase = false;
    public string nextScene;
    public Y_FadeIn fadein;
    public Y_TutorialImageContainer container;
    public GameObject last;
    public bool isChanging = false;
    private static GameObject m_last;
    private static Y_FadeIn m_fadein;
    private static Y_TutorialImageContainer m_container;
    public static Y_Continue instance;

    private void Start()
    {
        instance = this;
        m_container = container;
        m_fadein = fadein;
        m_last = last;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!Y_OpeningLevelController.isPressed && collision.gameObject.name.Contains("Laser"))
        {
            Debug.Log("Hit");
            Y_OpeningLevelController.isPressed = true;
            StartCoroutine(changeScene());
        }
    }*/

    public IEnumerator changeScene()
    {
        fadein.fade();
        yield return new WaitForSeconds(3f);

        if(isSpecialCase)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene);
        }
        else
        {
            Y_GlobalGameController.LoadNextLevel();
        }
        
    }
}
