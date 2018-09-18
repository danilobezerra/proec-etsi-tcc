using UnityEngine;

namespace Game.Scripts
{
    public class BlockGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _blockPrefab;

        public Block Generate(Block.Feature features)
        {
            var instance = InstantiateBlock();
            var block = instance.GetComponent<Block>();
            block.Features = features;
        
            return block;
        }

        private GameObject InstantiateBlock()
        {
            var instance = Instantiate(_blockPrefab);
            instance.transform.SetParent(gameObject.transform, false);
        
            return instance;
        }
    }
}
