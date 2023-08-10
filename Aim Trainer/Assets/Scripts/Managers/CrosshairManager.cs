using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ECrosshairTypes {
    SMALL,
    MEDIUM,
    LARGE
}

public class CrosshairManager : MonoBehaviour{
    [SerializeField] private Button crosshairSmallButton;
    [SerializeField] private Button crosshairMediumButton;
    [SerializeField] private Button crosshairLargeButton;

    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    

    private void Awake() {
        Debug.Log(crosshairTypeList);
        crosshairSmallButton.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[0].crosshairImage, crosshairTypeList.crosshairTypeList[0].width, crosshairTypeList.crosshairTypeList[0].height);
        });
        crosshairMediumButton.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[1].crosshairImage, crosshairTypeList.crosshairTypeList[1].width, crosshairTypeList.crosshairTypeList[1].height);
        });
        crosshairLargeButton.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[2].crosshairImage, crosshairTypeList.crosshairTypeList[2].width, crosshairTypeList.crosshairTypeList[2].height);
        });
    }
}
