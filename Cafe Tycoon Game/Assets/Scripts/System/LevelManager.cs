using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Level;
    [SerializeField]
    private MachineObject[] sellerObjects;
    [SerializeField]
    private TableObject[] sittingObjects;

    [SerializeField]
    private GameObject[] robotBuyerPrefab;
    [SerializeField]
    private Transform robotSpawnPoint;
    [SerializeField]
    private float spawnTimer;

    private void Start()
    {
        StartCoroutine(Timer());
    }
    public void SpawnBuyer()
    {
        Transform machineQueue = null, tableQueue = null;
        MachineObject machine = null;
        TableObject table = null;

        foreach (var seller in sellerObjects)
        {
            if (seller.IsQueueFree && seller.IsAvaliable)
            {
                machineQueue = seller.GetFreeQueuePlace();
                machine = seller;
                break;
            }
        }
        foreach (var place in sittingObjects)
        {
            if (place.IsQueueFree && place.IsAvaliable)
            {
                tableQueue = place.GetFreeQueuePlace();
                table = place;
                break;
            }
        }

        if (machine != null && table != null)
        {
            var robot = Instantiate(robotBuyerPrefab[Random.Range(0, robotBuyerPrefab.Length)]
                , robotSpawnPoint.position, Quaternion.identity, null);

            robot.GetComponent<RobotBuyerController>().SetPath(machineQueue, tableQueue);
            machine.AddPersonToQueue(robot.GetComponent<RobotBuyerController>());
            table.AddPersonToQueue(robot.GetComponent<RobotBuyerController>());
        }
    }
    private IEnumerator Timer()
    {
        SpawnBuyer();
        yield return new WaitForSeconds(spawnTimer);

        StartCoroutine(Timer());
    }
}
