using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Table Pop up Settings")]
    public TablePopUpHolder TableUpdatePopUp;
    [Header("Machine Pop up Settings")]
    public MachinePopUpHolder MachineUpdatePopUp;

    public void SetAndShowMachinePopUp(DescriptionBase description, int level, MachineObject machineObject)
    {
        MachineUpdatePopUp.SetPopUp(description, level, machineObject);
        MachineUpdatePopUp.gameObject.SetActive(true);
        TableUpdatePopUp.gameObject.SetActive(false);
    }
    public void SetAndShowTablePopUp(DescriptionBase description, TableObject tableObject)
    {
        TableUpdatePopUp.SetPopUp(description, tableObject);
        TableUpdatePopUp.gameObject.SetActive(true);
        MachineUpdatePopUp.gameObject.SetActive(false);
    }
}
