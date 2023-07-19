using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private int countdownTime;

    [SerializeField] private GameObject[] UIElements;
    [SerializeField] private PlayerGun playerGun;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown() {
        playerGun.enabled = false;
        foreach (var item in UIElements) {
            item.SetActive(false);
        }
        
        
        while (countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownDisplay.gameObject.SetActive(false);

        foreach (var item in UIElements) {
            item.SetActive(true);
        }
        playerGun.enabled = true;

        GameManager.instance.timerIsRunning = true;
    }
}
