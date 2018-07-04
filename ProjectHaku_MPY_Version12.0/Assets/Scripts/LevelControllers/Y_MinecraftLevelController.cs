using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Y_MinecraftLevelController : MonoBehaviour {

    public float PlayTime;
    public float RandomMin;
    public float RandomMax;

    [Header("For Cleanup")]
    public P_ControllerGrabObject rightController;
    public P_ControllerGrabObject leftController;
    public Y_C8763Checker audioContainer;
    public Renderer hurtCapsule;
    public AudioSource returnAudioSource;
    public AudioSource C8763AudioSource;
    public GameObject GameCanvas;
    public UnityEngine.UI.Text CountDownDialog;
    public Y_FadeIn fadeOut;
    

    private static Y_MinecraftLevelController instance;
    private static bool isNightMare = false;
    private static bool playedStartEffect = false;
    
    private static float count = 0;
    private static float score = 0;
    private static float m_min;
    private static float m_max;
    private static float m_Time;
    private static Y_C8763Checker m_audioContainer;
    private static Renderer m_hurtCapsule;
    private static AudioSource m_returnAudioSource;
    private static AudioSource m_C8763AudioSource;
    private static Y_FadeIn m_fadeOut;
    private static GameObject m_GameCanvas;
    private static UnityEngine.UI.Text m_dialog;
    private static UnityEngine.UI.Text m_CountDownDialog;
    private static P_ControllerGrabObject m_rightController;
    private static P_ControllerGrabObject m_leftController;

    private static bool isGameStart = false;

    private bool isSpawning = false;
    private bool playedStartEffectEnded = false;

    public static Y_SpawnCreepers creeperController;
    
    
    private void Awake()
    {
        count = 0;
        score = 0;
        isSpawning = false;
        playedStartEffectEnded = false;
        isGameStart = false;
        isNightMare = false;
        playedStartEffect = false;
        instance = this;
    }

    // Api for GlobalGameController.AdjustGoodEndThreshold()
    public static void init()
    {
        count = score = 0;
        isGameStart = false;
        playedStartEffect = false;
        isNightMare = false;
        if (instance) {
            m_Time = instance.PlayTime;
            instance.isSpawning = false;
            instance.playedStartEffectEnded = false;
        }

}

void Start () {
        creeperController = GameObject.Find("SpawnPoints").GetComponent<Y_SpawnCreepers>();
        m_Time = PlayTime;
        m_max = RandomMax;
        m_min = RandomMin;
        m_audioContainer = audioContainer;
        m_hurtCapsule = hurtCapsule;
        m_returnAudioSource = returnAudioSource;
        m_C8763AudioSource = C8763AudioSource;
        m_GameCanvas = GameCanvas;
        m_dialog = m_GameCanvas.GetComponentInChildren<UnityEngine.UI.Text>();
        m_fadeOut = fadeOut;
        m_CountDownDialog = CountDownDialog;
        m_leftController = leftController;
        m_rightController = rightController;
    }
	
	void Update () {
        if (isGameStart)
        {
            if (!playedStartEffect)
            {
                playedStartEffect = true;
                StartCoroutine(GameStartCanvasEffect());
                return;
            }

            // spawn after game start effect fading done
            if(playedStartEffectEnded)
                GameLogic();

            // For Debug
            PlayTime = count;
        }
	}

    IEnumerator GameStartCanvasEffect()
    {
        m_dialog.text = "Game Start!";
        m_GameCanvas.GetComponent<Animator>().SetTrigger("Next");
        yield return new WaitForSeconds(2f);

        m_dialog.text = "";
        m_GameCanvas.GetComponent<Animator>().ResetTrigger("Next");

        playedStartEffectEnded = true;
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameOver();
        //}
    }

    private void GameOver()
    {
        isGameStart = false;

        // CleanUp Scene

        if (m_rightController)
        {
            m_rightController.ForceRealease();
            m_rightController.enabled = false;
        }

        if (m_leftController)
        {
            m_leftController.ForceRealease();
            m_leftController.enabled = false;
        }

        GameObject[] creepers = GameObject.FindGameObjectsWithTag("Creepers");

        foreach(GameObject g in creepers)
        {
            UnityEngine.AI.NavMeshAgent nav = g.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (nav)
            {
                nav.enabled = false;
            }

            Y_CreeperMovementAnimation y = g.GetComponent<Y_CreeperMovementAnimation>();
            if (y)
            {
                y.isWalking = false;
            }

            Y_CreeperLookAt x = g.GetComponentInChildren<Y_CreeperLookAt>();
            if (x)
            {
                x.enabled = false;
            }
        }

        Debug.Log("GameOver! Score = " + score);

        Y_GlobalGameController.incScore(score);

        StartCoroutine(playGameOverEffects());

    }

    private static IEnumerator playGameOverEffects()
    {

        m_GameCanvas.GetComponent<Animator>().Play("Idle");

        yield return new WaitForSeconds(.5f);

        m_dialog.text = "Game Over!";

        yield return new WaitForSeconds(.5f);

        // Play Anim
        m_GameCanvas.GetComponent<Animator>().SetTrigger("Next");

        yield return new WaitForSeconds(1.5f);

        m_C8763AudioSource.Stop();
        m_returnAudioSource.Stop();
        m_hurtCapsule.gameObject.SetActive(false);
        m_audioContainer.nowPlay.ToList().ForEach(x => x.Stop());

        m_fadeOut.fade();

        yield return new WaitForSeconds(3f);

        // Change Scene
        Y_GlobalGameController.LoadNextLevel();
    }

    public static void incScore(string tag)
    {
        if(tag == "DiamondSword")
            score += 2;
        else
            score += 1;
    }

    IEnumerator spawn(float sec)
    {
        isSpawning = true;
        
        if (isNightMare)
        {
            creeperController.SpawnAll();
            yield return new WaitForSeconds(1);
        }
        else{
            creeperController.Spawn();
            yield return new WaitForSeconds(sec);
        }
        
        isSpawning = false;
    }

    private void GameLogic()
    {
     
        count += Time.deltaTime;
        m_CountDownDialog.text = "剩餘時間：" + ((int)(Mathf.Clamp(m_Time - count, 0f, m_Time))).ToString();

        if (count < m_Time)
        {
            // Ready to go!
            if (!isSpawning)
            {
                StartCoroutine(spawn(Random.Range(m_min, m_max)));                
            }

            if(count > m_Time * (2f/3f))
            {
                isNightMare = true;
            }
            
        }
        else
        {
            GameOver();
        }
    }

    public static void StartGame()
    {
        isGameStart = true;

        // creeperController.Spawn();
    }
    
    private IEnumerator cleanupCoroutine()
    {
        m_C8763AudioSource.Stop();
        m_returnAudioSource.Stop();
        m_hurtCapsule.gameObject.SetActive(false);
        m_audioContainer.nowPlay.ToList().ForEach(x => x.Stop());
        yield return new WaitForSeconds(1f);
        Camera.main.transform.parent.transform.position = new Vector3(0f, 4.55f, 24f);
    }

    public static void cleanup()
    {
        instance.StartCoroutine(instance.cleanupCoroutine());
    }
}

