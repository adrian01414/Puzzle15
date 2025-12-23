namespace Puzzle15
{
    public struct CellMoveData
    {
        public readonly int Cell_i;
        public readonly int Cell_j;
        public readonly int To_i;
        public readonly int To_j;

        public CellMoveData(int cell_i, int cell_j, int to_i, int to_j)
        {
            Cell_i = cell_i;
            Cell_j = cell_j;
            To_i = to_i;
            To_j = to_j;
        }
    }
}