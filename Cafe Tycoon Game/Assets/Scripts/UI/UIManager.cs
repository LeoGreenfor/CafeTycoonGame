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
    [Header("Counter Pop up Settings")]
    public CounterPopUpHolder CounterUpdatePopUp;

    public void SetAndShowMachinePopUp(DescriptionBase description, int level, MachineObject machineObject)
    {
        MachineUpdatePopUp.SetPopUp(description, level, machineObject);
        MachineUpdatePopUp.gameObject.SetActive(true);
    }
    public void SetAndShowTablePopUp(DescriptionBase description)
    {
        TableUpdatePopUp.SetPopUp(description);
        TableUpdatePopUp.gameObject.SetActive(true);
    }
    public void SetAndShowCounterPopUp(DescriptionBase description, int price)
    {
        CounterUpdatePopUp.SetPopUp(description, price);
        CounterUpdatePopUp.gameObject.SetActive(true);
    }
}
