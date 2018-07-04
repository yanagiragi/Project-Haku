using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_StartCreeperGame : MonoBehaviour {
    public Y_SpawnPlayerAtPoint spawn;
    private void OnTriggerEnter(Collider other)
    {
        spawn.adjustPosSpin(spawn.spawnPoint);
        Y_MinecraftLevelController.StartGame();
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
