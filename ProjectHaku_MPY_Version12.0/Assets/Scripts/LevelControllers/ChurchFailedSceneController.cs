using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchFailedSceneController : MonoBehaviour {

    public Y_FinalSceneCanvasTextControl mainCanvasTextControl;
    public Y_FinalSceneCanvasTextControl subCanvasTextControl;
    public ChurchFailedSceneLaser churchFailedSceneLaserLeft;
    public ChurchFailedSceneLaser churchFailedSceneLaserRight;
    public DropDownIpad dropDownIpad;
    public AudioSource partOneMusic;
    public AudioSource partTwoMusic;
    public GameObject LeftController;
    public GameObject RightController;

    public Kino.DigitalGlitch digital;
    public Y_FadeAnimPlay fade;

    public static bool canChoose = false;
    public static bool isChoosed = false;
    public static bool isContinue = false;
    
    // Use this for initialization
    void Start () {
        subCanvasTextControl.strs[0] = "";
        subCanvasTextControl.strs[1] = ">\t放棄\n\t永不放棄";
        subCanvasTextControl.strs[2] = ">\t放棄\n\t永不放棄";
        subCanvasTextControl.strs[3] = "";
        subCanvasTextControl.strs[4] = "";

        LeftController.GetComponent<P_LaserPointer>().enabled = false;
        RightController.GetComponent<P_LaserPointer>().enabled = false;
        LeftController.GetComponent<P_ControllerGrabObject>().enabled = false;
        RightController.GetComponent<P_ControllerGrabObject>().enabled = false;

        digital.enabled = false;
        canChoose = false;
        isChoosed = false;
        isContinue = false;
        StartCoroutine(wrapper());

    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator wrapper()
    {
        // 吶
        yield return new WaitForSeconds(3f);
        mainCanvasTextControl.UpdateText();

        // 這樣就可以了嗎?
        yield return new WaitForSeconds(5f);
        mainCanvasTextControl.UpdateText();

        // 就這樣看著她被搶走也可以嗎?
        yield return new WaitForSeconds(5f);
        mainCanvasTextControl.UpdateText();

        // 如果...
        yield return new WaitForSeconds(5f);
        
        mainCanvasTextControl.UpdateText();

        yield return new WaitForSeconds(2f);
        float volume = partOneMusic.volume;
        partOneMusic.volume = 0;
        partOneMusic.Play();

        yield return new WaitForSeconds(.1f);
        partOneMusic.volume = volume;

        // 如果還有機會
        yield return new WaitForSeconds(5f);
        mainCanvasTextControl.UpdateText();

        // 你會願意賭上一切
        yield return new WaitForSeconds(5f);
        mainCanvasTextControl.UpdateText();

        yield return new WaitForSeconds(3.5f);

        digital.enabled = true;
        yield return new WaitForSeconds(0.5f);
        digital.enabled = false;

        yield return new WaitForSeconds(1f);

        // 只為了扭轉這個結局嗎?        
        mainCanvasTextControl.UpdateText();

        // ...
        yield return new WaitForSeconds(5f);
        subCanvasTextControl.UpdateText();
        mainCanvasTextControl.UpdateText();

        yield return new WaitForSeconds(1f);
        canChoose = true;

        float time = 0;

        //while(isChoosed == false && time < 20f)
        //{
        //    time += Time.deltaTime;
        //    yield return null;
        //}
        //time = 0;


        //// yield return new WaitForSeconds(20f);
        //if (!isChoosed)
        //{
        //    canChoose = false;
        //    subCanvasTextControl.UpdateText();
        //    yield return new WaitForSeconds(.5f);
        //    canChoose = true;
        //}
        //else
        //{
        //    subCanvasTextControl.UpdateText();
        //}


        while (isChoosed == false)
        {
            yield return null;
        }

        while (isContinue)
        {
            subCanvasTextControl.strs[1] = ">\t放棄\n\t永不放棄";
            subCanvasTextControl.Backward();

            mainCanvasTextControl.strs[8] = "你真的不打算放棄嗎?";
            mainCanvasTextControl.Backward();

            yield return new WaitForSeconds(1f);

            isChoosed = false;
            mainCanvasTextControl.UpdateText();
            subCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(1.5f);

            canChoose = true;
            churchFailedSceneLaserLeft.enabled = true;
            churchFailedSceneLaserRight.enabled = true;

            while (!isChoosed)
            {
                yield return null;
            }

            if (!isContinue)
            {
                /*subCanvasTextControl.strs[1] = ">\t放棄\n";
                subCanvasTextControl.Backward();
                subCanvasTextControl.UpdateText();
                */
                //yield return new WaitForSeconds(2f);
                break;
            }

            // 明明只是個遊戲\n你卻要賭上「一切」?

            subCanvasTextControl.strs[1] = "";
            subCanvasTextControl.Backward();

            mainCanvasTextControl.strs[8] = "";
            mainCanvasTextControl.Backward();

            yield return new WaitForSeconds(1f);

            isChoosed = false;
            mainCanvasTextControl.UpdateText();
            // Clear Subcanvas
            subCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(2f);

            time = 0;
            string target = "明明只是個遊戲\n    你卻要賭上「一切」嗎?";

            while (time < 5f)
            {
                time += Time.deltaTime;
                int index = (int)(time / 5.0f * target.Length);
                if(index < target.Length)
                    mainCanvasTextControl.gameObject.GetComponent<UnityEngine.UI.Text>().text = target.Substring(0, index).Replace(" ","");
                else
                    mainCanvasTextControl.gameObject.GetComponent<UnityEngine.UI.Text>().text = target.Replace(" ", "");
                yield return null;
            }

            //yield return new WaitForSeconds(5f);
            subCanvasTextControl.strs[1] = ">\t放棄\n\t永不放棄";
            subCanvasTextControl.Backward();
            subCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(2.5f);

            canChoose = true;
            churchFailedSceneLaserLeft.enabled = true;
            churchFailedSceneLaserRight.enabled = true;

            while (!isChoosed)
            {
                yield return null;
            }

            if (!isContinue)
            {
                //subCanvasTextControl.strs[1] = ">\t放棄\n";
                //subCanvasTextControl.strs[2] = "";
                //subCanvasTextControl.Backward();
                //subCanvasTextControl.UpdateText();

                //yield return new WaitForSeconds(1f);
                break;
            }
            else
            {
                subCanvasTextControl.strs[1] = "\n>\t永不放棄";
                subCanvasTextControl.strs[2] = "";
                subCanvasTextControl.Backward();
                subCanvasTextControl.UpdateText();
                yield return new WaitForSeconds(1f);
            }

            // 我們已經收到你的想法
            yield return new WaitForSeconds(5.5f);
            mainCanvasTextControl.UpdateText();
            subCanvasTextControl.UpdateText();

            partTwoMusic.Play();
            // reassign variable to allow \n to work
            mainCanvasTextControl.strs[10] = "也許想要拯救她的想法\n只是自我滿足";

            subCanvasTextControl.UpdateText();
            mainCanvasTextControl.UpdateText();

            // 也許想要拯救她的想法\n只是自我滿足
            yield return new WaitForSeconds(4f);
            mainCanvasTextControl.UpdateText();

            // 但是即使如此都不願意放棄
            yield return new WaitForSeconds(4f);
            mainCanvasTextControl.UpdateText();

            // 那就去做吧
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 你將挑戰困難的任務
            yield return new WaitForSeconds(4f);
            mainCanvasTextControl.UpdateText();

            // 也許你會覺得這是垃圾遊戲
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 但是如果你不把它當作遊戲
            yield return new WaitForSeconds(4f);
            mainCanvasTextControl.UpdateText();

            // 如果你非見到結局不可
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 如果你非見到她不可
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();


            // 那就不要放棄持續挑戰吧
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // ...以上幾點
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 還請多多包涵。
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(1f);
            dropDownIpad.canStart = true;

            LeftController.GetComponent<P_LaserPointer>().enabled = true;
            RightController.GetComponent<P_LaserPointer>().enabled = true;
            LeftController.GetComponent<P_ControllerGrabObject>().enabled = true;
            RightController.GetComponent<P_ControllerGrabObject>().enabled = true;

            break; // all passed
        }
        
        if(!isContinue)
        {
            while (isChoosed == false && isContinue)
            {
                if (partOneMusic.isPlaying && time > 4f)
                {
                    partOneMusic.volume -= Time.deltaTime;
                }
                time += Time.deltaTime;
                yield return null;
            }

            subCanvasTextControl.strs[2] = ">\t放棄\n";
            subCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(2f);

            subCanvasTextControl.strs[3] = "";
            //subCanvasTextControl.UpdateText();

            mainCanvasTextControl.strs[10] = "你選擇了放棄";
            mainCanvasTextControl.strs[11] = "感謝你的遊玩";
            mainCanvasTextControl.strs[12] = "這個遊戲會在幾秒後自動關閉";
            mainCanvasTextControl.strs[13] = "下次見。";

            // 我們已經收到你的想法
            yield return new WaitForSeconds(5f);
            subCanvasTextControl.UpdateText();
            mainCanvasTextControl.UpdateText();

            time = 0;

            while(time < 5f)
            {
                if(partOneMusic.isPlaying)
                {
                    partOneMusic.volume = Mathf.Lerp(partOneMusic.volume, 0.0f, Time.deltaTime);
                }
                time += Time.deltaTime;
                yield return null;
            }

            partOneMusic.volume = 0;

            // 你選擇了放棄
            //yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 感謝你的遊玩
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 這個遊戲會在幾秒後自動關閉
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            // 下次見。
            yield return new WaitForSeconds(5f);
            mainCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(2f);

            // go to final Bg and go to beginning
            fade.ShouldChangeSceneToFinalBg();

            yield return new WaitForSeconds(1f);

            if (!Application.isEditor)
                Application.Quit();
            else
            {
                Time.timeScale = 0;
                Debug.Log("Quitted");
            }
        }
    }
}
