using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }
    List<HighScoreElement> highScoreList = new List<HighScoreElement>();
    [SerializeField] int maxCount = 10;
    [SerializeField] string fileName;

    public delegate void OnHighscoreListChanged(List<HighScoreElement> list);
    public static event OnHighscoreListChanged onHighScoreListChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        LoadHighScores();
    }
    private void LoadHighScores()
    {
        highScoreList = FileHandler.ReadListFromJSON<HighScoreElement>(fileName);
        while (highScoreList.Count > maxCount)
        {
            highScoreList.RemoveAt(maxCount);
        }

        if (onHighScoreListChanged != null)
        {
            onHighScoreListChanged.Invoke(highScoreList);
        }
    }
    public int getTopScore()
    {
        return highScoreList[0].score;
    }
    private void SaveHighScores()
    {
        FileHandler.SaveToJSON<HighScoreElement>(highScoreList, fileName);
    }

    public void AddHighScore(HighScoreElement highScore)
    {
        for (int i = 0; i < maxCount; i++)
        {
            //add new highscore
            if (i >= highScoreList.Count || highScore.score > highScoreList[i].score)
            {
                highScoreList.Insert(i, highScore);
            }

            while (highScoreList.Count > maxCount)
            {
                highScoreList.RemoveAt(maxCount);
            }
            SaveHighScores();
            if (onHighScoreListChanged != null)
            {
                onHighScoreListChanged.Invoke(highScoreList);
            }
            break;
        }
    }
}
