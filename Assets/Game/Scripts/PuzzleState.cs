namespace Game.Scripts
{
    public class PuzzleState
    {
        private readonly int[,] _board;

        public bool IsSolvable
        {
            get
            {
                for (var i = 0; i < _board.GetLength(0); i++) {
                    for (var j = 0; j < _board.GetLength(1); j++) {
                        var features = (Block.Feature) _board[i, j];

                        if (features.HasFlag(Block.Feature.Source)) {
                            return Solve(features, Block.Feature.Source, i, j);
                        }
                    }
                }

                return false;
            }
        }

        public PuzzleState(int[,] board)
        {
            _board = board;
        }
    
        private bool Solve(Block.Feature current, Block.Feature expected, int row, int column)
        {
            if (!current.HasFlag(expected)) return false;

            if (Block.HasFeaturesPair(current, expected, Block.Feature.Top)) {
                if (row - 1 >= 0 && !Block.IsEmpty(_board[row - 1, column])) {
                    var next = (Block.Feature) _board[row - 1, column];
                    return Solve(next, Block.Feature.Bottom, row - 1, column);
                }
            }
        
            if (Block.HasFeaturesPair(current, expected, Block.Feature.Bottom)) {
                if (row + 1 < _board.GetLength(0) && !Block.IsEmpty(_board[row + 1, column])) {
                    var next = (Block.Feature) _board[row + 1, column];
                    return Solve(next, Block.Feature.Top, row + 1, column);
                }
            }
        
            if (Block.HasFeaturesPair(current, expected, Block.Feature.Left)) {
                if (column - 1 >= 0 && !Block.IsEmpty(_board[row, column - 1])) {
                    var next = (Block.Feature) _board[row, column - 1];
                    return Solve(next, Block.Feature.Right, row, column - 1);
                }
            }
        
            if (Block.HasFeaturesPair(current, expected, Block.Feature.Right)) {
                if (column + 1 < _board.GetLength(1) && !Block.IsEmpty(_board[row, column + 1])) {
                    var next = (Block.Feature) _board[row, column + 1];
                    return Solve(next, Block.Feature.Left, row, column + 1);
                }
            }

            return Block.HasFeaturesPair(current, expected, Block.Feature.Target);
        }
    }
}
