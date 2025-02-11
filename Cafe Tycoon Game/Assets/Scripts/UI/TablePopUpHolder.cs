using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TablePopUpHolder : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;
    [Header("Buttons")]
    [SerializeField] private Button addChairButton;

    public void SetPopUp(DescriptionBase description)
    {
        Title.text = description.Title;
        Description.text = description.Description;
    }
}
