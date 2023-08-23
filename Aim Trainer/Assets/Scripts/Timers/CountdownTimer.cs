using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private int countdownTime;
    [SerializeField] private GameObject crosshair;

    public event EventHandler OnCountdownTimerStopped;

    private void Start(){
        StartCountdown();
    }

    public void StartCountdown() {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {
        GameManager.Instance.GetPlayerGun().enabled = false;
        crosshair.SetActive(false);
        
        while (countdownTime > 0) {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownDisplay.gameObject.SetActive(false);

        crosshair.SetActive(true);

        GameManager.Instance.GetPlayerGun().enabled = true;

        OnCountdownTimerStopped?.Invoke(this, EventArgs.Empty);
    }
}
