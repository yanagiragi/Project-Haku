using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_FinalSceneCanvasTextControl : MonoBehaviour {

    public bool isChurchScene;
    public bool shouldTeleportImm = false;
    public Y_FadeAnimPlay changePos;
    public string[] strs;

    private int index = 0;
    private UnityEngine.UI.Text text;
    private Animator anim;

    void Start () {
        anim = GetComponentInParent<Animator>();
        text = GetComponent<UnityEngine.UI.Text>();
        text.text = strs[index];
	}

    public void Backward()
    {
        index--;
    }

    public void UpdateText () {
        StartCoroutine(wrapper());
	}

    IEnumerator wrapper()
    {
        anim.SetBool("Next", true);
        yield return new WaitForEndOfFrame();
        anim.SetBool("Next", false);

        yield return new WaitForSeconds(1f);

        index = Mathf.Clamp(index, 0, strs.Length - 2);
        text.text = strs [++index];

        if(index == strs.Length - 1 && isChurchScene)
        {
            yield return new WaitForSeconds(3f);
            changePos.ShouldChangeSceneToChurch();

            if (shouldTeleportImm)
            {
                yield return new WaitForSeconds(10f);
                changePos.ShouldChangeSceneToFinalBg();
            }
        }
    }
}
