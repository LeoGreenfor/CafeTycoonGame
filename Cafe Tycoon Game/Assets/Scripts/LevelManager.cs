using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Level;
    [SerializeField]
    private SellersObject[] sellerObjects;
    [SerializeField]
    private SittingObject[] sittingObjects;

    [SerializeField]
    private GameObject robotBuyerPrefab;
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
        SellersObject machine = null;
        SittingObject table = null;

        foreach (var seller in sellerObjects)
        {
            if (seller.IsQueueFree)// && seller.IsAvaliable)
            {
                machineQueue = seller.GetFreeQueuePlace();
                machine = seller;
                break;
            }
        }
        foreach (var place in sittingObjects)
        {
            if (place.IsQueueFree)// && place.IsAvaliable)
            {
                tableQueue = place.GetFreeQueuePlace();
                table = place;
                break;
            }
        }

        if (machine != null && table != null)
        {
            var robot = Instantiate(robotBuyerPrefab, robotSpawnPoint.position, Quaternion.identity, null);

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
