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
    [SerializeField] private string priceLevelUpProfitTemplate;
    [SerializeField] private Button priceLevelUpButton;
    [SerializeField] private Image priceFillLevel;
    [SerializeField] private TMP_Text priceLabel;
    [SerializeField] private string priceLevelUpQueueTemplate;
    [SerializeField] private Button queueCapacityLevelUpButton;
    [SerializeField] private Image queueFillLevel;
    [SerializeField] private TMP_Text queueLabel;

    private MachineObject machine;

    private void Awake()
    {
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

        queueLabel.text = $"{priceLevelUpQueueTemplate} {machine.GetPriceForQueueLevelUp()}$";
        priceLabel.text = $"{priceLevelUpProfitTemplate} {machine.GetPriceForProfitsLevelUp()}$";

        buyItemPopUp.SetPopUp(machine.GetItemPrice(), machine);
        buyItemPopUp.gameObject.SetActive(!machine.IsAvaliable);

        if (!machine.IsQueueFullLevelUp) queueCapacityLevelUpButton.interactable = true;
        else queueCapacityLevelUpButton.interactable = false;
    }

    private async void LevelUpPrice()
    {
        float newPrice = machine.GetPriceForProfitsLevelUp();

        if (newPrice > GameManager.Instance.Money)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Error);
            return;
        }
        SoundManager.Instance.PlaySound(SoundManager.SoundType.PressButton);

        GameManager.Instance.Money -= newPrice;
        priceFillLevel.fillAmount += 0.1f;

        float newProfit = machine.GetProfit();
        newProfit = GameManager.Instance.LinearGrowth(newProfit);
        newPrice = GameManager.Instance.LinearGrowth(newPrice);

        if (priceFillLevel.fillAmount >= 1)
        {
            priceLevelUpButton.interactable = false;

            newProfit = GameManager.Instance.ExponentialGrowth(newProfit);
            newPrice = GameManager.Instance.ExponentialGrowth(newPrice);

            await Task.Delay(1000);
            priceFillLevel.fillAmount = 0;
            priceLevelUpButton.interactable = true;

            SoundManager.Instance.PlaySound(SoundManager.SoundType.LevelUp);
        }

        machine.UpdProfits(newProfit);
        machine.UpdPriceForProfitsLevelUp(newPrice);

        priceLabel.text = "Current profit: " + newProfit + "/per";
        priceLabel.text = $"{priceLevelUpProfitTemplate} {newPrice}$";
    }
    private async void QueueCapacityLevelUp()
    {
        float newPrice = machine.GetPriceForQueueLevelUp();

        if (newPrice > GameManager.Instance.Money)
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Error);
            return;
        }
        SoundManager.Instance.PlaySound(SoundManager.SoundType.PressButton);

        GameManager.Instance.Money -= newPrice;
        newPrice = GameManager.Instance.LinearGrowth(newPrice);

        queueFillLevel.fillAmount += 0.1f;
        if (queueFillLevel.fillAmount >= 1)
        {
            queueCapacityLevelUpButton.interactable = false;
            machine.UpdMaxQueueSize();
            
            newPrice = GameManager.Instance.ExponentialGrowth(newPrice);
            
            await Task.Delay(1000);
            queueFillLevel.fillAmount = 0;
            if (!machine.IsQueueFullLevelUp) queueCapacityLevelUpButton.interactable = true;
            else GameManager.Instance.Level++;

            SoundManager.Instance.PlaySound(SoundManager.SoundType.LevelUp);
        }

        machine.UpdPriceForQueueLevelUp(newPrice);

        queueLabel.text = $"{priceLevelUpQueueTemplate} {newPrice}$";
    }

    private void OnDestroy()
    {
        priceLevelUpButton?.onClick.RemoveAllListeners();
        queueCapacityLevelUpButton?.onClick.RemoveAllListeners();
    }
}
