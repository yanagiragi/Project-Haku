using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_SpawnCreepers : MonoBehaviour {
    public GameObject creeperPrefab;
    public Transform[] spawnTransforms;
    
    public void Spawn () {
        int index = Random.Range(0, spawnTransforms.Length);
        GameObject g = null;

        if (creeperPrefab)
            g = GameObject.Instantiate(creeperPrefab);

        if (g)
        {
            g.transform.position = spawnTransforms[index].transform.position;
            g.name += spawnTransforms[index].gameObject.name;
        }
    }

    public void SpawnAll()
    {
        foreach (Transform t in spawnTransforms)
        {
            if (creeperPrefab)
            {
                GameObject g = GameObject.Instantiate(creeperPrefab);
                g.transform.position = t.transform.position;
                g.name += t.gameObject.name;
            }            
        }
    }
}
