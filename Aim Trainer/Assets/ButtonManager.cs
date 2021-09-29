using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private TextMeshProUGUI TrainingHeader;

    private void Update() {
        foreach (Button btn in buttons) {
            Button choice = btn; // need to store in separate variable to have the right button in the lamda expression
            btn.onClick.AddListener(() => TaskOnClick(choice));
        }
    }

    void TaskOnClick(Button choice) {
        if (choice.gameObject.tag == "BTN_headshot") {
            TrainingHeader.text = "Headshot Practice";
        }
        else if (choice.gameObject.tag == "BTN_motion") {
            TrainingHeader.text = "Motion Practice";
        }
    }

}
