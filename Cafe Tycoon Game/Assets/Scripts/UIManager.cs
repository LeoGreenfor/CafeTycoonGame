using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Table Pop up Settings")]
    public GameObject TableUpdatePopUp;
    [Header("Machine Pop up Settings")]
    public GameObject MachineUpdatePopUp;
    [SerializeField] private TMP_Text machineTitle;
    [SerializeField] private Image machineIcon;
    [SerializeField] private TMP_Text machineDescription;
    [SerializeField] private TMP_Text machineLevel;
    [Header("Counter Pop up Settings")]
    public GameObject CounterUpdatePopUp;

    public void SetAndShowMachinePopUp(DescriptionBase description, int level)
    {
        machineTitle.text = description.Title;
        machineIcon.sprite = description.Icon;
        machineDescription.text = description.Description;
        machineLevel.text = level.ToString();

        MachineUpdatePopUp.SetActive(true);
    }
}
