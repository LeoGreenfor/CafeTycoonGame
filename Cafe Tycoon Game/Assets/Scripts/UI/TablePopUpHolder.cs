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
        buyItemPopUp.gameObject.SetActive(false);
        addChairButton?.onClick.AddListener(AddChairToTable);
    }

    public void SetPopUp(DescriptionBase description, TableObject tableObject)
    {
        Title.text = description.Title;
        Description.text = description.Description;

        table = tableObject;

        if (!table.IsAvaliable) buyItemPopUp.gameObject.SetActive(true);

        ShowChairsIcons(table.GetLastAddedChairNumber());
        if (!table.IsFullOfChairs) addChairButton.interactable = true;
        else addChairButton.interactable = false;
    }

    private async void AddChairToTable()
    {
        fillImage.fillAmount += 0.1f;

        if (fillImage.fillAmount >= 1)
        {
            addChairButton.interactable = false;
            table.UpdMaxQueueSize();
            await Task.Delay(2000);
            fillImage.fillAmount = 0;
            chairsIcons[table.GetLastAddedChairNumber()].gameObject.SetActive(true);
            if (!table.IsFullOfChairs) addChairButton.interactable = true;
        }
    }

    private void ShowChairsIcons(int unlockedChairsCount)
    {
        foreach (var chair in chairsIcons)
        {
            chair.gameObject.SetActive(false);
        }

        for (int i = 0; i <= unlockedChairsCount; i++)
        {
            chairsIcons[i].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        addChairButton?.onClick.RemoveAllListeners();
    }
}
