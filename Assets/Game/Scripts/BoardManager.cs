using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(BlockGenerator))]
    public class BoardManager : MonoBehaviour
    {
        private BlockGenerator _blockGenerator;

        private delegate Block GenerateBlock(Block.Feature features);

        private void Awake()
        {
            _blockGenerator = GetComponent<BlockGenerator>();
        }
    
        private void Start()
        {
            var stage = GameManager.Instance.CurrentStageName;
            var loadStreamingAsset = FileReader.LoadStreamingAsset(stage, values => {
                ValuesToBoard(values, _blockGenerator.Generate);
            });
            
            StartCoroutine(loadStreamingAsset);
        }

        private static Block[] ValuesToBoard(int[,] values, GenerateBlock generateBlock)
        {
            var index = 0;
            var width = values.GetLength(0);
            var height = values.GetLength(1);
            var board = new Block[width * height];
        
            for (var i = 0; i < height; i++) {
                for (var j = 0; j < width; j++) {
                    board[index] = generateBlock((Block.Feature) values[i, j]);
                    index++;
                }
            }
        
            return board;
        }
    
        private static int[,] BoardToValues(IReadOnlyList<Block> board)
        {
            var index = 0;
            var length = (int) Mathf.Sqrt(board.Count);
            var values = new int[length, length];
        
            for (var i = 0; i < length; i++) {
                for (var j = 0; j < length; j++) {
                    values[i, j] = (int) board[index].Features;
                    index++;
                }
            }
        
            return values;
        }
    
        public void SolvePuzzle()
        {
            var values = BoardToValues(transform.GetComponentsInChildren<Block>());
            var state = new PuzzleState(values);

            if (!state.IsSolvable) return;
        
            GameManager.Instance.NextStage();
        }
    }
}
