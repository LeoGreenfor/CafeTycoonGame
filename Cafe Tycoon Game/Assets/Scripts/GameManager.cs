using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Stats")]
    [SerializeField]
    private float money;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            moneyLabel.text = money.ToString();
        }
    }
    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            levelLabel.text = level.ToString();
        }
    }
    [SerializeField]
    private float multiplier;
    [Header("Stats UI")]
    [SerializeField]
    private TMP_Text moneyLabel;
    [SerializeField]
    private TMP_Text levelLabel;

    private void Start()
    {
        Money = money;        
    }

    public float LinearGrowth(float price)
    {
        return price + (multiplier * Level);
    }
    public float ExponentialGrowth(float price)
    {
        return price * Mathf.Pow(multiplier, Level);
    }
}
