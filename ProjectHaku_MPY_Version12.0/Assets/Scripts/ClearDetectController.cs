using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
using System;
using System.Runtime.Serialization;

namespace yrProjectHaku
{
    [Serializable]
    public class yrSerializable
    {
        private string _Salt;
        public yrSerializable()
        {
            this._Salt = "this is a book";
        }
        public yrSerializable(string val)
        {
            this._Salt = "Yowane";
        }
        public yrSerializable(int val)
        {
            this._Salt = "Haku";
        }

        public string Salt
        {
            get { return this._Salt; }
        }
    }
}

public class ClearDetectController : MonoBehaviour {

    //public static string dataPath;
    public static string dataPath = "./.YanagiCrashReports";
    public static void Lock()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs;
        
        if (File.Exists(dataPath))
        {
            fs = new FileStream(dataPath, FileMode.Open);

            FileInfo dataFileInfo = new FileInfo(dataPath);

            // Remove the hidden attribute of the file
            dataFileInfo.Attributes &= ~FileAttributes.Hidden;

            try
            {
                yrProjectHaku.yrSerializable data = new yrProjectHaku.yrSerializable("lock");

                formatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to serialize. Reason: " + e.Message);
                UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
                return;
            }
            finally
            {
                dataFileInfo.Attributes |= FileAttributes.Hidden;
                fs.Close();
            }
        }
        else
        {
            Debug.LogError("Failed to serialize. Reason: " + dataPath + " does not exists.");
        }
    }

    public static void Success()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs;

        if (File.Exists(dataPath))
        {
            fs = new FileStream(dataPath, FileMode.Open);

            FileInfo dataFileInfo = new FileInfo(dataPath);

            // Remove the hidden attribute of the file
            dataFileInfo.Attributes &= ~FileAttributes.Hidden;

            try
            {
                yrProjectHaku.yrSerializable data = new yrProjectHaku.yrSerializable(1);

                formatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to serialize. Reason: " + e.Message);
                UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
                return;
            }
            finally
            {
                dataFileInfo.Attributes |= FileAttributes.Hidden;
                fs.Close();
            }
        }
        else
        {
            Debug.LogError("Failed to serialize. Reason: " + dataPath + " does not exists.");
        }
    }

    void Awake () {

        FileInfo dataFileInfo = new FileInfo(dataPath);

        yrProjectHaku.yrSerializable data;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs;

        // create for the first time
        if (!File.Exists(dataPath))
        {
            fs = new FileStream(dataPath, FileMode.Create);
            
            // Remove the hidden attribute of the file
            dataFileInfo.Attributes &= ~FileAttributes.Hidden;

            try
            {
                data = new yrProjectHaku.yrSerializable();
                
                formatter.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to serialize. Reason: " + e.Message);
                UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
                return;
            }
            finally
            {
                dataFileInfo.Attributes |= FileAttributes.Hidden;
                fs.Close();
            }
        }

        fs = new FileStream(dataPath, FileMode.Open);
        // Remove the hidden attribute of the file
        dataFileInfo.Attributes &= ~FileAttributes.Hidden;

        try
        {
            // Deserialize the hashtable from the file and 
            // assign the reference to the local variable.
            data = (yrProjectHaku.yrSerializable)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to deserialize. Reason: " + e.Message);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
            return;
            // throw;
        }
        finally
        {
            dataFileInfo.Attributes |= FileAttributes.Hidden;
            fs.Close();
        }

        if(data.Salt == "Yowane")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SpecialOpening");
        }
        else if (data.Salt == "Haku")
        {
            Y_GlobalGameController.AdjustGoodEndThreshold();
            UnityEngine.SceneManagement.SceneManager.LoadScene("ClearOpening");
        }

    }
}
