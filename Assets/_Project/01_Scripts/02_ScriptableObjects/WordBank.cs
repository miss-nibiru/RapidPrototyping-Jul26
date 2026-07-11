using UnityEngine;
public class WordBank : MonoBehaviour
{
    public static WordBank Instance { get; private set; }
    [Header("Word Prefabs (6 total)")]
    [SerializeField] private WordObject wordPrefabOne;
    [SerializeField] private WordObject wordPrefabTwo;
    [SerializeField] private WordObject wordPrefabThree;
    [SerializeField] private WordObject wordPrefabFour;
    [SerializeField] private WordObject wordPrefabFive;
    [SerializeField] private WordObject wordPrefabSix;
    [Header("Spawn Points (6 total)")]
    [SerializeField] private Transform spawnPointOne;
    [SerializeField] private Transform spawnPointTwo;
    [SerializeField] private Transform spawnPointThree;
    [SerializeField] private Transform spawnPointFour;
    [SerializeField] private Transform spawnPointFive;
    [SerializeField] private Transform spawnPointSix;
    private WordObject[] _spawnedWords;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SpawnWords()
    {
        ClearWords();
        _spawnedWords = new WordObject[6];
        _spawnedWords[0] = Instantiate(wordPrefabOne, spawnPointOne);
        _spawnedWords[1] = Instantiate(wordPrefabTwo, spawnPointTwo);
        _spawnedWords[2] = Instantiate(wordPrefabThree, spawnPointThree);
        _spawnedWords[3] = Instantiate(wordPrefabFour, spawnPointFour);
        _spawnedWords[4] = Instantiate(wordPrefabFive, spawnPointFive);
        _spawnedWords[5] = Instantiate(wordPrefabSix, spawnPointSix);
    }
    public void ClearWords()
    {
        // If _spawnedWords is null, there's nothing to clear yet
        if (_spawnedWords == null) return;
        
        // Loop through and destroy each word
        for (int i = 0; i < _spawnedWords.Length; i++)
        {
            if (_spawnedWords[i] != null)
            {
                // Explicitly return word to spawn before destroying
                _spawnedWords[i].ReturnToSpawn();
                Destroy(_spawnedWords[i].gameObject);
                _spawnedWords[i] = null;
            }
        }
        
        // Clear the array reference
        _spawnedWords = null;
    }
}

public enum responceType
{
    GOOD,
    BAD,
    NEUTRAL,
}
