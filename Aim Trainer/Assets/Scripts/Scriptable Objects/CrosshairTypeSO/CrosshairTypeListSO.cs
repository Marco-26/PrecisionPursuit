using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CrosshairTypeList")]
public class CrosshairTypeListSO : ScriptableObject{
    public List<CrosshairTypeSO> crosshairTypeList;
}
