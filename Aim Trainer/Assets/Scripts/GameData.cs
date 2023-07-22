using UnityEngine;

public enum Gamemode
{
    TIMER_BASED,
    TARGET_BASED,
    UNSELECTED
}

public class GameData : MonoBehaviour
{
    public int totalTargets { get; private set; }
    public Gamemode currentGamemode { get; private set; } = Gamemode.UNSELECTED;

    public static GameData instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTotalTargets(int amount){
        this.totalTargets = amount;
    }

    public void SetGamemode(Gamemode gamemode){
        this.currentGamemode = gamemode;
    }
}
