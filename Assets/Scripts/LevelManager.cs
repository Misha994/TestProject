using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int point;
    [SerializeField] private int amountShapes;
    [SerializeField] private int nextScoreThreshold = 5;
    [SerializeField] private float speed;

    [SerializeField] private UIManager uIManager;
    [SerializeField] private SpawnManager spawnManager;

    private int ScoreThreshold;

    private void Start()
    {
        uIManager.startMenu += StartLevel;
        uIManager.stopMenu += ResetLevel;
        ScoreThreshold = nextScoreThreshold;
    }

    public void NewBeground(Sprite[] backgrounds)
    {
        uIManager.SetBeground(backgrounds);
    }


    public void IncreaseLevel()
    {
        level++;
        speed += 0.1f;
        point++;
        amountShapes++;
        spawnManager.ChangeLevel(point, speed, amountShapes);
        uIManager.ChangeBackground();
        uIManager.ChangeLevelText(level);
    }

    public void StartLevel()
    {
        spawnManager.StartSpawn();
    }

    public void ResetLevel()
    {
        spawnManager.DeleteAllShapes();
        spawnManager.StopSpawn();
        level = 1;
        speed = 0;
        point = 0;
        amountShapes = 0;
        ScoreThreshold = nextScoreThreshold;
        uIManager.ResetLevel();
        spawnManager.ChangeLevel(point, speed, amountShapes);
    }

    private void Update()
    {
        if (uIManager.GetScore() > ScoreThreshold)
        {
            ScoreThreshold += nextScoreThreshold;
            IncreaseLevel();

        }
    }

}
