using UnityEngine;
using System.Collections.Generic;
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
    
    [Header("Word Options Bank")]
    [SerializeField] private List<WordOptions> wordOptions = new();
    private WordObject[] _spawnedWords;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
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

        List<WordOptions> randomOptions = PossibleResponceOptions();

        for (int i = 0; i < _spawnedWords.Length; i++)
        {
            if (_spawnedWords[i] != null && i < randomOptions.Count)
            {
                _spawnedWords[i].AssignResponce(randomOptions[i]);
            }
        }
    }
    
    private List<WordOptions> PossibleResponceOptions()
    {
        List<WordOptions> chosenOptions = new List<WordOptions>();

        chosenOptions.AddRange(GetRandomOptionsByType(ResponceType.Aggressive, 2));
        chosenOptions.AddRange(GetRandomOptionsByType(ResponceType.Casual, 2));
        chosenOptions.AddRange(GetRandomOptionsByType(ResponceType.Formal, 2));

        ShuffleWordOptions(chosenOptions);

        return chosenOptions;
    }

    private List<WordOptions> GetRandomOptionsByType(ResponceType type, int amount)
    {
        List<WordOptions> matchingOptions = new List<WordOptions>();
        List<WordOptions> chosenOptions = new List<WordOptions>();

        foreach (WordOptions option in wordOptions)
        {
            if (option != null && option.responceType == type)
            {
                matchingOptions.Add(option);
            }
        }

        while (chosenOptions.Count < amount && matchingOptions.Count > 0)
        {
            int randomIndex = Random.Range(0, matchingOptions.Count);

            chosenOptions.Add(matchingOptions[randomIndex]);
            matchingOptions.RemoveAt(randomIndex);
        }

        return chosenOptions;
    }

    private void ShuffleWordOptions(List<WordOptions> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            int randomIndex = Random.Range(i, options.Count);
            (options[i], options[randomIndex]) = (options[randomIndex], options[i]);
        }
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

public enum ResponceType
{
    Formal,
    Casual,
    Aggressive,
}
