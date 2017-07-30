using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;

public class ArduinoController : MonoBehaviour
{
    public UnityEngine.UI.Text voltageDisplayText;
    public int touchThreshold = 100;
    public int hurtThreshold = 300;
    
    public bool isTouching = false;
    public bool isHurt = false;

    public bool isdebug = true;
    public int isTouchingFalseCount = 0;
    private int val = 0;
    private SerialPortThread myThread;
    
    void Start()
    {
        myThread = new SerialPortThread(this);
        new Thread(myThread.run).Start();
    }

    public void PassDebug(int val)
    {
        if (val > touchThreshold)
        {
            isTouching = true;
            isTouchingFalseCount = 0;
        }
        else
        {
            isTouchingFalseCount++;
        }

        if (val > hurtThreshold)
        {
            isHurt = true;
        }
        else
        {
            isHurt = false;
        }
            

        this.val = val;
    }

    void Update()
    {
        if (isdebug)
        {
            voltageDisplayText.text = val.ToString();
        }

        if(isTouchingFalseCount > 150 && isTouching)
        {
            isTouching = false;
            isHurt = false;
        }
    }

    void OnApplicationQuit()
    {
        myThread.isRunning = false;
    }
}

class SerialPortThread
{
    public bool isRunning = true;
    private ArduinoController upperClass;

    public SerialPortThread(ArduinoController upperClass)
    {
        this.upperClass = upperClass;
    }

    public void run()
    {
        SerialPort sp = new SerialPort(@"\\.\COM12", 9600);
        
        sp.ReadTimeout = 50;
        try
        {
            sp.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        while (isRunning)
        {
            string text = "";
            try
            {
                text = sp.ReadLine();
                
                upperClass.PassDebug(Int32.Parse(text));

            }
            catch (Exception e)
            {
                
            }
        }

        sp.Close();
    }
}