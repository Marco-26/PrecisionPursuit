using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Crosshair")]
public class CrosshairTypeSO : ScriptableObject{
    public Sprite crosshairImage;
    public int width, height;
}
