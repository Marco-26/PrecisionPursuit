using UnityEngine;

public static class GameValues
{
    public enum Gamemode {TimerBased, TargetBased, Unselected}
    public static Gamemode currentGamemode;
    public static int maxKillCount = 15;
    public static int totalShots = 0;
    public static int hitShots = 0;
}
