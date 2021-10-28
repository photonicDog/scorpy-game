using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Types;
using Flags;
using UnityEngine;

public class SaveDataLink : MonoBehaviour
{
    public void LoadGame() {
        SaveData sd = SaveDataManager.Instance.LoadData();
        
        FlagManager.Instance.flags.SetAllFlags(sd.Flags);
        InventoryManager.Instance.inventory = sd.Inventory;
        SceneManager.Instance.SwitchLevel(sd.Level);
        SaveDataManager.Instance.SetPlayerPositionToSave(sd);
    }
}
