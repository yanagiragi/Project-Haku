using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_NadeDetect : MonoBehaviour {

    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter happy, sad;
    public ArduinoController arduino;
	
	
	// Update is called once per frame
	void Update () {

        if (arduino.isTouching)
        {
            if (!happy.isProcessing)
            {
                playMorph(happy);
            }
        }
        else
        {
            if (happy.isProcessing)
            {
                stopMorph(happy);
            }
        }

        if (arduino.isHurt)
        {
            if (happy.isProcessing)
                stopMorph(happy);

            if (!sad.isProcessing)
            {
                playMorph(sad);
            }
        }
        else
        {
            if (sad.isProcessing)
            {
                stopMorph(sad);
            }
        }
    }

    void playMorph(MYB.MMD4MecanimJitter.MMD4M_MorphJitter happy)
    {
        happy.loopGroupEnabled = true;
        happy.PlayLoop();
    }

    void stopMorph(MYB.MMD4MecanimJitter.MMD4M_MorphJitter happy)
    {
        happy.loopGroupEnabled = false;
        happy.StopLoop();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerStay(Collider collision)
    {
        //if(!happy.IsInvoking())
        
    }

    private void OnTriggerExit(Collider collision)
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
