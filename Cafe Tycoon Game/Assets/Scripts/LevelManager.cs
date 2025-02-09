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
        //var spawnPosition = sellerObjects[0].GetFreeQueuePlace();
        var robot = Instantiate(robotBuyerPrefab, robotSpawnPoint.position, Quaternion.identity, null);
        robot.GetComponent<RobotBuyerController>()
            .SetPath(sellerObjects[0].GetFreeQueuePlace(), sittingObjects[0].GetFreeQueuePlace());
        sellerObjects[0].UpdCurrentQueueSize(1);
        sittingObjects[0].UpdCurrentQueueSize(1);
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnBuyer();

        StartCoroutine(Timer());
    }
}
