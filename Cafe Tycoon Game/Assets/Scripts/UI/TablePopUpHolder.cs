using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TablePopUpHolder : MonoBehaviour
{
    [SerializeField] private BuyItemPopUpHolder buyItemPopUp;
    [Header("Info")]
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private Image[] chairsIcons;
    [Header("Buttons")]
    [SerializeField] private Button addChairButton;
    [SerializeField] private Image fillImage;

    private TableObject table;

    private void Awake()
    {
        addChairButton?.onClick.AddListener(AddChairToTable);
    }

    public void SetPopUp(DescriptionBase description, TableObject tableObject)
    {
        Title.text = description.Title;
        Description.text = description.Description;

        table = tableObject;

        buyItemPopUp.SetPopUp(table.GetItemPrice(), table);
        buyItemPopUp.gameObject.SetActive(!table.IsAvaliable);

        ShowChairsIcons(table.GetLastAddedChairNumber());
        if (!table.IsFullOfChairs) addChairButton.interactable = true;
        else addChairButton.interactable = false;
    }

    private async void AddChairToTable()
    {
        float newPrice = table.GetPriceForQueueLevelUp();

        if (newPrice > GameManager.Instance.Money) return;

        GameManager.Instance.Money -= newPrice;
        newPrice = GameManager.Instance.LinearGrowth(newPrice);

        fillImage.fillAmount += 0.1f;

        if (fillImage.fillAmount >= 1)
        {
            addChairButton.interactable = false;
            table.UpdMaxQueueSize();

            newPrice = GameManager.Instance.ExponentialGrowth(newPrice);

            await Task.Delay(2000);
            fillImage.fillAmount = 0;
            chairsIcons[table.GetLastAddedChairNumber()].gameObject.SetActive(true);
            if (!table.IsFullOfChairs) addChairButton.interactable = true;
            else GameManager.Instance.Level++;
        }

        table.UpdPriceForQueueLevelUp(newPrice);
    }

    private void ShowChairsIcons(int unlockedChairsCount)
    {
        foreach (var chair in chairsIcons)
        {
            chair.gameObject.SetActive(false);
        }

        chairsIcons[0].gameObject.SetActive(true);

        for (int i = 1; i <= unlockedChairsCount; i++)
        {
            chairsIcons[i].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        addChairButton?.onClick.RemoveAllListeners();
    }
}
