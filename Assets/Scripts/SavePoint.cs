using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public void Save() {
        Transform player = GameObject.FindWithTag("Player").transform;
        SaveDataManager.Instance.SaveData(player.position);
    }
}
