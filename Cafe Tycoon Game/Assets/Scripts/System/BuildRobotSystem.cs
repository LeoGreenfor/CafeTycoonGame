using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRobotSystem : MonoBehaviour
{
    private bool _isCanBuild;
    public bool IsCanBuild
    {
        get
        {
            return _isCanBuild;
        }
        set
        {
            _isCanBuild = value;
            if (value) buildButton.image.sprite = canBuildSprite;
            else buildButton.image.sprite = cantBuildSprite;
        }
    }
    [SerializeField]
    private Button buildButton;
    [SerializeField]
    private Sprite canBuildSprite;
    [SerializeField]
    private Sprite cantBuildSprite;

    [SerializeField]
    private GameObject buildRobotPrefab;
    [SerializeField]
    private Transform buildSpawnPoint;

    private void Start()
    {
        buildButton.onClick.AddListener(BuildRobot);
    }

    public void BuildRobot()
    {
        if (_isCanBuild && !GameManager.Instance.IsHaveRobotStatue)
        {
            Instantiate(buildRobotPrefab, buildSpawnPoint.position, Quaternion.identity, null);
            GameManager.Instance.IsHaveRobotStatue = true;
            GameManager.Instance.ReduceCostForBuild();
        }
    }
}
