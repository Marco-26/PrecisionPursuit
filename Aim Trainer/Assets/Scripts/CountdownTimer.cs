using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private Text countdownDisplay;
    [SerializeField] private int countdownTime;

    [SerializeField] private GameObject[] gameObjects;

    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown() {
        HideGameobjects();
        while (countdownTime >= 0) {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        countdownDisplay.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownDisplay.gameObject.SetActive(false);
        ActivateGameobjects();
    }

    void HideGameobjects() {
        foreach (var item in gameObjects) {
            item.SetActive(false);
        }
    }

    void ActivateGameobjects() {
        foreach (var item in gameObjects) {
            item.SetActive(true);
        }
    }
}
