using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePopUpHolder : MonoBehaviour
{
    [SerializeField] private BuyItemPopUpHolder buyItemPopUp;
    [Header("Info")]
    [SerializeField] private TMP_Text Title;
    [SerializeField] private Image Icon;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text Level;
    [Header("Buttons")]
    [SerializeField] private Button priceLevelUpButton;
    [SerializeField] private Image priceFillLevel;
    [SerializeField] private TMP_Text priceLabel;
    [SerializeField] private Button queueCapacityLevelUpButton;
    [SerializeField] private Image queueFillLevel;
    [SerializeField] private TMP_Text queueLabel;

    private MachineObject machine;

    private void Awake()
    {
        buyItemPopUp.gameObject.SetActive(false);
        priceLevelUpButton?.onClick.AddListener(LevelUpPrice);
        queueCapacityLevelUpButton?.onClick.AddListener(QueueCapacityLevelUp);
    }

    public void SetPopUp(DescriptionBase description, int level, MachineObject machineObject)
    {
        Title.text = description.Title;
        Icon.sprite = description.Icon;
        Description.text = description.Description;
        Level.text = level.ToString();

        machine = machineObject;

        if (!machine.IsAvaliable) buyItemPopUp.gameObject.SetActive(true);

        if (!machine.IsQueueFullLevelUp) queueCapacityLevelUpButton.interactable = true;
        else queueCapacityLevelUpButton.interactable = false;
    }

    private async void LevelUpPrice()
    {
        priceFillLevel.fillAmount += 0.1f;
        if (priceFillLevel.fillAmount >= 1)
        {
            priceLevelUpButton.interactable = false;
            machine.UpdPricePerItem();
            await Task.Delay(2000);
            priceFillLevel.fillAmount = 0;
            priceLevelUpButton.interactable = true;
            priceLabel.text = "Current price: " + machine.GetCurrentPrice();
        }
    }
    private async void QueueCapacityLevelUp()
    {
        queueFillLevel.fillAmount += 0.1f;
        if (queueFillLevel.fillAmount >= 1)
        {
            queueCapacityLevelUpButton.interactable = false;
            machine.UpdMaxQueueSize();
            await Task.Delay(2000);
            queueFillLevel.fillAmount = 0;
            if (!machine.IsQueueFullLevelUp) queueCapacityLevelUpButton.interactable = true;
        }
    }

    private void OnDestroy()
    {
        priceLevelUpButton?.onClick.RemoveAllListeners();
        queueCapacityLevelUpButton?.onClick.RemoveAllListeners();
    }
}
