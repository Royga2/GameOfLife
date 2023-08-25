namespace GameOfLife.Model
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int LiveNeighborCount { get; set; }

        public Cell()
        {
            IsAlive = false;
            LiveNeighborCount = 0;
        }
    }
}
