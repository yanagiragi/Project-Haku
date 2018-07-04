using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour {

    public float playCount = 0;

    public static GlobalGameManager Instance = null;
    private GlobalGameManager m_instance;

    public static bool hitBoxMode = false;
    public static bool masterMode = false;

    public bool m_hitBoxMode = false;
    public bool m_MasterMode = false;

    static bool created = false;
    
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

        m_instance = this;

        if (Instance == null)
            Instance = m_instance;
    }

    void Update() { 
    //{
    //    if (hitBoxToggle && hitBoxToggle.enabled)
    //    {
    //        hitBoxMode = hitBoxToggle.isOn;
    //        m_hitBoxMode = hitBoxMode;
    //    }

    //    if (masterToggle && masterToggle.enabled)
    //    {
    //        masterMode = masterToggle.isOn;
    //        m_MasterMode = masterMode;
    //    }
        

    }

    public static void UpdatePlayCount (bool win)
    {
        GlobalGameManager.Instance.playCount += win ? 1f : 0.5f;	
	}
}
