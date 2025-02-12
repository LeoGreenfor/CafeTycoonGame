using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemPopUpHolder : MonoBehaviour
{
    [SerializeField] private float Price;
    [SerializeField] private string buttonLabelTemplate;
    [SerializeField] private Button buyItemButton;
    [SerializeField] private TMP_Text buttonLabel;

    private InteractibleObject item;

    private void Awake()
    {
        buyItemButton?.onClick.AddListener(BuyItem);
    }

    public void SetPopUp(float price, InteractibleObject interactibleObject)
    {
        Price = price;
        item = interactibleObject;
        buttonLabel.text = buttonLabelTemplate + " " + Price;
    }

    private void BuyItem()
    {
        float price = item.GetItemPrice();
        if (price > GameManager.Instance.Money) return;

        GameManager.Instance.Money -= price;
        GameManager.Instance.Level++;
        item.IsAvaliable = true;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        buyItemButton?.onClick.RemoveAllListeners();
    }
}
