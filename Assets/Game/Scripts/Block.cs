using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class Block : MonoBehaviour
    {
        [Flags]
        public enum Feature
        {
            None = 0,
            Top = 0x01,
            Bottom = 0x02,
            Right = 0x04,
            Left = 0x08,
            Target = 0x10,
            Source = 0x20
        }

        [SerializeField] private Image[] _images;
        [SerializeField] private Color[] _colors;

        private Image _background;
        private Feature _features;
    
        public Feature Features
        {
            get => _features;
            set => SetFeatures(value);
        }

        private void Awake()
        {
            _background = GetComponent<Image>();
        }

        private void SetFeatures(Feature features)
        {
            _features = features;
            UpdateFeatures();
        }

        private void ResetFeatures()
        {
            _background.color = _colors[0];
        
            foreach (var image in _images) {
                image.gameObject.SetActive(false);
            }
        }

        private void UpdateFeatures()
        {
            var features = Enum.GetValues(typeof(Feature));
        
            foreach (Feature feature in features) {
                if (!_features.HasFlag(feature)) continue;
            
                _images[0].gameObject.SetActive(true);
                _background.color = _colors[1];
                
                switch (feature) {
                    case Feature.None:
                        ResetFeatures();
                        continue;
                    case Feature.Top:
                        _images[1].gameObject.SetActive(true);
                        break;
                    case Feature.Bottom:
                        _images[2].gameObject.SetActive(true);
                        break;
                    case Feature.Right:
                        _images[3].gameObject.SetActive(true);
                        break;
                    case Feature.Left:
                        _images[4].gameObject.SetActive(true);
                        break;
                    case Feature.Target:
                        _background.color = _colors[2];
                        break;
                    case Feature.Source:
                        _background.color = _colors[3];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static bool IsEmpty(int value)
        {
            var feature = (Feature) value;
            return feature == Feature.None;
        }
    
        public static bool HasFeaturesPair(Feature value, Feature a, Feature b)
        {
            return (value | (a | b)) == (a | b);
        }
    }
}
