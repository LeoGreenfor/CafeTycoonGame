using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public float money;
    public MachineObjectData[] machinesData;
    public TableObjectData[] tablesData;

    public GameData(MachineObjectData[] machines, TableObjectData[] tables)
    {
        level = GameManager.Instance.Level;
        money = GameManager.Instance.Money;
        machinesData = machines;
        tablesData = tables;
    }
    public GameData(MachineObject[] machines, TableObject[] tables)
    {
        level = GameManager.Instance.Level;
        money = GameManager.Instance.Money;
        machinesData = MachineToData(machines);
        tablesData = TableToData(tables);
    }

    private MachineObjectData[] MachineToData(MachineObject[] machineObjects)
    {
        MachineObjectData[] objectDatas = new MachineObjectData[machineObjects.Length];

        for (int i = 0; i < machineObjects.Length; i++)
        {
            objectDatas[i] = new MachineObjectData(machineObjects[i].IsAvaliable, machineObjects[i].GetMaxQueueSize(),
                machineObjects[i].GetProfit(), machineObjects[i].GetPriceForProfitsLevelUp(), 
                machineObjects[i].GetPriceForQueueLevelUp());
        }

        return objectDatas;
    }
    private TableObjectData[] TableToData(TableObject[] tableObjects)
    {
        TableObjectData[] objectDatas = new TableObjectData[tableObjects.Length];

        for (int i = 0; i < tableObjects.Length; i++)
        {
            objectDatas[i] = new TableObjectData(tableObjects[i].IsAvaliable,
                tableObjects[i].GetMaxQueueSize(), tableObjects[i].GetPriceForQueueLevelUp());
        }

        return objectDatas;
    }

    public MachineObject[] DataToMachine(MachineObject[] machines)
    {
        for (int i = 0; i < machinesData.Length; i++)
        {
            machines[i].IsAvaliable = machinesData[i].IsAvaliable;
            machines[i].UpdMaxQueueSize(machinesData[i].MaxQueueSize);
            machines[i].UpdProfits(machinesData[i].Profits);
            machines[i].UpdPriceForProfitsLevelUp(machinesData[i].PriceForProfitsLevelUp);
            machines[i].UpdPriceForQueueLevelUp(machinesData[i].PriceForQueueLevelUp);
        }

        return machines;
    }
    public TableObject[] DataToTable(TableObject[] table)
    {
        for (int i = 0; i < tablesData.Length; i++)
        {
            table[i].IsAvaliable = tablesData[i].IsAvaliable;
            table[i].UpdMaxQueueSize(tablesData[i].MaxQueueSize);
            table[i].UpdPriceForQueueLevelUp(tablesData[i].PriceForQueueLevelUp);
        }

        return table;
    }

    [System.Serializable]
    public class MachineObjectData
    {
        public bool IsAvaliable;
        public int MaxQueueSize;
        public float Profits;
        public float PriceForProfitsLevelUp;
        public float PriceForQueueLevelUp;

        public MachineObjectData(bool isAvaliable, int maxQueueSize, float profits, 
            float priceForProfitsLevelUp, float priceForQueueLevelUp)
        {
            IsAvaliable = isAvaliable;
            MaxQueueSize = maxQueueSize;
            Profits = profits;
            PriceForProfitsLevelUp = priceForProfitsLevelUp;
            PriceForQueueLevelUp = priceForQueueLevelUp;
        }
    }
    [System.Serializable]
    public class TableObjectData
    {
        public bool IsAvaliable;
        public int MaxQueueSize;
        public float PriceForQueueLevelUp;

        public TableObjectData(bool isAvaliable, int maxQueueSize, float priceForQueueLevelUp)
        {
            IsAvaliable = isAvaliable;
            MaxQueueSize = maxQueueSize;
            PriceForQueueLevelUp = priceForQueueLevelUp;
        }
    }
}
