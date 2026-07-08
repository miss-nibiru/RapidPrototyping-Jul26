using UnityEngine;

public class WordBank : MonoBehaviour
{
    public static WordBank Instance { get; private set; }

    [Header("Available Words)")]
    [SerializeField] private WordObject wordOne;
    [SerializeField] private WordObject wordTwo;
    [SerializeField] private WordObject wordThree;
    [SerializeField] private WordObject wordFour;

    private WordObject[] _words;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _words = new WordObject[] { wordOne, wordTwo, wordThree, wordFour };
    }

    public WordObject[] GetAllWords()
    {
        return _words;
    }
}

public enum responceType
{
    GOOD,
    BAD,
    NEUTRAL,
}