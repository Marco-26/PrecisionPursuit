using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/GameSettingsSO")]
public class GameSettignsSO : ScriptableObject{
    public Gamemode chosenGamemode;
    public CrosshairTypeSO currentCrosshair;
}
