namespace GameOfLife.Model
{
    public class Cell
    {
        #region Properties

        public bool IsAlive { get; set; }
        public int LiveNeighborCount { get; set; }

        #endregion


        #region Constructors

        public Cell()
        {
            IsAlive = false;
            LiveNeighborCount = 0;
        }

        #endregion
    }
}
