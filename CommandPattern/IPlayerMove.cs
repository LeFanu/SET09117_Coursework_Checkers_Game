namespace CommandPattern
{
    public interface IPlayerMove
    {
        void execute(int x, int y);
        void undo();
    }
}