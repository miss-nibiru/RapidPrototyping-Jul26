using UnityEngine;
namespace _Project._01_Scripts._00_VisualScripts
{
    public class SlotManager : MonoBehaviour
    {
        public static SlotManager Instance;
        
        [Header("Slots")]
        [SerializeField] private Transform slotOne;
        [SerializeField] private Transform slotTwo;
        
        private WordObject _wordInSlotOne;
        private WordObject _wordInSlotTwo;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
        public Transform[] GetAllSlots()
        {
            return new[] { slotOne, slotTwo };
        }
        
        public bool TryPlaceWord(WordObject word, Transform targetSlot)
        {
            if (word.placed)
            {
                return false;
            }
            
            if (targetSlot == slotOne)
            {
                if (_wordInSlotOne != null)
                {
                    _wordInSlotOne.ReturnToSpawn();
                }
                
                _wordInSlotOne = word;
                word.rectTransform.SetParent(slotOne);
                word.rectTransform.anchoredPosition = Vector3.zero;
                return true;
            }
            
            if (targetSlot == slotTwo)
            {
                if (_wordInSlotTwo != null)
                {
                    _wordInSlotTwo.ReturnToSpawn();
                }
                
                _wordInSlotTwo = word;
                word.rectTransform.SetParent(slotTwo);
                word.rectTransform.anchoredPosition = Vector3.zero;
                return true;
            }
            
            return false;
        }
        
        public bool AreSlotsFilled()
        {
            return _wordInSlotOne != null && _wordInSlotTwo != null;
        }
        
        public WordObject[] GetSelectedWords()
        {
            return new[] { _wordInSlotOne, _wordInSlotTwo };
        }
        public void RemoveWordFromSlot(WordObject word)
        {
            if (_wordInSlotOne == word)
            {
                _wordInSlotOne = null;
            }
            else if (_wordInSlotTwo == word)
            {
                _wordInSlotTwo = null;
            }
        }
        
        public WordObject GetWordInSlot(Transform slot)
        {
            if (slot == slotOne) return _wordInSlotOne;
            if (slot == slotTwo) return _wordInSlotTwo;
            return null;
        }
        
        public void ClearSlots()
        {
            if (_wordInSlotOne != null)
            {
                _wordInSlotOne.ReturnToSpawn();
            }
            
            if (_wordInSlotTwo != null)
            {
                _wordInSlotTwo.ReturnToSpawn();
            }
            _wordInSlotOne = null;
            _wordInSlotTwo = null;
        }
    }
}