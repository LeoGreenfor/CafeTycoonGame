using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsHaveRobotStatue;
    [Header("Stats")]
    [SerializeField]
    private float money;
    private float lastCheckedMoney = 0f;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            money = Mathf.Round(money * 10f) / 10f;
            moneyLabel.text = money.ToString();

            while (lastCheckedMoney + 20f <= money)
            {
                lastCheckedMoney += 20f;
                AddRobotPiece();
            }
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
    [Header("Build Robot")]
    [SerializeField] 
    private BuildRobotSystem buildRobotSystem;
    public int RobotPieceCount = 0;
    [SerializeField]
    private int coastOfBuild;
    [SerializeField]
    private TMP_Text robotPieceLabel;


    private void Start()
    {
        Money = money;        
    }

    public float LinearGrowth(float price)
    {
        return Mathf.Round((price + (multiplier * Level)) * 10f) / 10f;
    }
    public float ExponentialGrowth(float price)
    {
        return Mathf.Round((price * Mathf.Pow(multiplier, Level)) * 10f) / 10f;
    }

    public void LoadRobotStatue()
    {
        if (IsHaveRobotStatue)
        {
            buildRobotSystem.BuildRobot();
            ReduceCostForBuild();
        }
    }

    public void ReduceCostForBuild()
    {
        RobotPieceCount -= coastOfBuild;
        RobotPieceCount = Math.Clamp(RobotPieceCount, 0, RobotPieceCount);
        robotPieceLabel.text = RobotPieceCount.ToString();
    }

    private void AddRobotPiece()
    {
        RobotPieceCount ++;
        robotPieceLabel.text = RobotPieceCount.ToString();

        if (RobotPieceCount >= coastOfBuild) buildRobotSystem.IsCanBuild = true;
        else buildRobotSystem.IsCanBuild = false;
    }
}
