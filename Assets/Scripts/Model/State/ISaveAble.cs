namespace Model.State
{
    public interface ISaveAble : IResetAble
    {
        string ToJson();
        void SaveFull();
        void Load();
    }
}