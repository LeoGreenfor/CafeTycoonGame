using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button startPlayButton;
    [SerializeField] 
    private float fillDuration = 2f;

    private Image targetImage;

    private void Start()
    {
        startPlayButton.onClick.AddListener(StartPlay);
        targetImage = startPlayButton.GetComponent<Image>();
    }

    private async void StartPlay()
    {
        float startFill = targetImage.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            targetImage.fillAmount = Mathf.Lerp(startFill, 0, elapsedTime / fillDuration);
            await Task.Yield();
        }

        targetImage.fillAmount = 0;

        SceneManager.LoadScene(1);
    }
}
