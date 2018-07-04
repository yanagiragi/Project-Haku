using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

        public Sprite winImage;
        public Sprite loseImage;
        public UnityEngine.UI.Image finalImage;

        public Health monsterHealth;
        public Health playerHealth;

        bool win;

        void Start()
        {
            win = monsterHealth.hp < playerHealth.hp;
            finalImage.sprite = (win) ? winImage : loseImage;
        }

        public void GameOver()
        {
            // Simple Hold States   
        }

        public void GameOverDone()
        {
            StartCoroutine(reloadLevel());
        }

        IEnumerator reloadLevel()
        {
            yield return new WaitForSeconds(2f);
            GlobalGameManager.UpdatePlayCount(win);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");
        }

}
