using UnityEngine;

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
        Instance = this;
    }

    public bool TryPlaceWord(WordObject word)
    {
        if (_wordInSlotOne == null)
        {
            _wordInSlotOne = word;
            word.transform.SetParent(slotOne);
            word.transform.localPosition = Vector3.zero;
            return true;
        }

        if (_wordInSlotTwo == null)
        {
            _wordInSlotTwo = word;
            word.transform.SetParent(slotTwo);
            word.transform.localPosition = Vector3.zero;
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

    public void ClearSlots()
    {
        _wordInSlotOne = null;
        _wordInSlotTwo = null;
    }
}