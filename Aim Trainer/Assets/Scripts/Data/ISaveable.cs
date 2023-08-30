namespace Data
{
    public interface ISaveable
    {
        void LoadData(SaveData data);
        void SaveData(SaveData data);
    }
}