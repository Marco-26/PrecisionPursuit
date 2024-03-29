using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Crosshair")]
public class CrosshairTypeSO : ScriptableObject{
    public Sprite sprite;
    public int width, height;
    public CrosshairType type;
}
