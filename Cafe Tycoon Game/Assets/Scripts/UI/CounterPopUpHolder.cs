using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterPopUpHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private Image Icon;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Price;

    public void SetPopUp(DescriptionBase description, int price)
    {
        Title.text = description.Title;
        Icon.sprite = description.Icon;
        Description.text = description.Description;
        Price.text = price.ToString();
    }

}
