using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOpeningLevelController : MonoBehaviour {

    public GameObject target;
    public Camera RenderCam;

    public Kino.AnalogGlitch analog;
    public Kino.DigitalGlitch digital;

    public UnityEngine.UI.Text text;
    public UnityEngine.UI.Text text2;
    
    public float interval;

    float time = 0;
    bool useAnalog = false;
    float count = 0;

    // Use this for initialization
    void Start () {
        digital.enabled = false;
        analog.enabled = false;

        StartCoroutine(wrapper());
    }

    IEnumerator wrapper()
    {
        yield return new WaitForSeconds(4f);

        digital.enabled = false;
        analog.enabled = true;

        yield return new WaitForSeconds(6f);

        RenderCam.Render();

        digital.enabled = false;
        analog.enabled = false;

        yield return new WaitForSeconds(1f);

        target.SetActive(false);
        digital.enabled = true;

        yield return new WaitForSeconds(2f);
        
        analog.enabled = true;

        yield return new WaitForSeconds(1f);

        digital.enabled = false;

        yield return new WaitForSeconds(7f);

        analog.enabled = true;
        digital.enabled = true;

        yield return new WaitForSeconds(3f);

        Shutdown();
        digital.enabled = false;
        analog.enabled = true;

    }

    // Update is called once per frame
    void Update () {
        //time += Time.deltaTime;

        //if(time > interval)
        //{
        //    ++count;
        //    updateGlitch();
        //    time = 0;
        //}

        //if(count >= 6)
        //{
        //    Shutdown();
        //    digital.enabled = false;
        //    analog.enabled = true;

        //    this.enabled = false;
        //}
	}

    void Shutdown()
    {
        text.text = "發生異常。發生異常。發生異常。";

        text2.text = "請關閉遊戲。\n\\n\n畢竟，\n你已經輸了。";
    }

    void updateGlitch()
    {
        useAnalog = !useAnalog;

        if (useAnalog)
        {
            digital.enabled = false;
            analog.enabled = true;
        }
        else
        {
            digital.enabled = true;
            analog.enabled = false;
        }
    }
}
