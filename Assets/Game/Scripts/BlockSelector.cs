using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class BlockSelector : MonoBehaviour
    {
        private static IList<Block> _selectedBlocks;
        private BoardManager _boardManager;

        private void Awake()
        {
            _selectedBlocks = new List<Block>();

            var obj = GameObject.FindGameObjectWithTag("Board");
            _boardManager = obj.GetComponent<BoardManager>();
        }

        public void SelectBlock()
        {
            var block = GetComponent<Block>();
            _selectedBlocks.Add(block);
        
            if (_selectedBlocks.Count > 1)
                CheckSelectedBlocks();
        }

        private void CheckSelectedBlocks()
        {
            if (IsNone()
                && !HasFeature(Block.Feature.Source)
                && !HasFeature(Block.Feature.Target)
                && !SameBlocks() && IsAdjacent())
                SwapSelectedBlocks();

            _selectedBlocks.Clear();
            _boardManager.SolvePuzzle();
        }

        private static bool IsNone()
        {
            var a = _selectedBlocks[0].GetComponent<Block>().Features;
            var b = _selectedBlocks[1].GetComponent<Block>().Features;
        
            return a == Block.Feature.None || b == Block.Feature.None;
        }

        private static bool HasFeature(Block.Feature feature)
        {
            var a = _selectedBlocks[0].GetComponent<Block>().Features;
            var b = _selectedBlocks[1].GetComponent<Block>().Features;
        
            return a.HasFlag(feature) || b.HasFlag(feature);
        }

        private static bool SameBlocks()
        {
            var a = _selectedBlocks[0].GetComponent<Block>();
            var b = _selectedBlocks[1].GetComponent<Block>();
        
            return a.Features == b.Features;
        }

        private bool IsAdjacent()
        {
            var length = (int) Mathf.Sqrt(_boardManager.transform.childCount);
        
            var a = _selectedBlocks[0].transform.GetSiblingIndex(); 
            var b = _selectedBlocks[1].transform.GetSiblingIndex();

            var onTop = (a == b - length);
            var onBottom = (a == b + length);
            var onLeft = (a == b - 1);
            var onRight = (a == b + 1);
        
            return onTop || onBottom || onLeft || onRight;
        }

        private static void SwapSelectedBlocks()
        {
            var a = _selectedBlocks[0].transform.GetSiblingIndex();
            var b = _selectedBlocks[1].transform.GetSiblingIndex();

            _selectedBlocks[0].transform.SetSiblingIndex(b);
            _selectedBlocks[1].transform.SetSiblingIndex(a);
        }
    }
}
