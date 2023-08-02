namespace Model.Identifier
{
    public interface IKey
    {
        string Key { get; }
        void SetAsKey();
    }
}