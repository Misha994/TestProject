using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text level;

    [SerializeField] private ShapeDestroyer shapeDestroyer;

    [SerializeField] private Button startButtton;
    [SerializeField] private Button stopButtton;

    [SerializeField] private GameObject PlayMenu;
    [SerializeField] private GameObject MainMenu;

    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Renderer background;

    public delegate void StartMenu();
    public StartMenu startMenu;

    public delegate void StopMenu();
    public StopMenu stopMenu;

    public delegate void ChackScore();
    public ChackScore chackScore;

    private float elapsedTime;
    private int currentSpriteIndex = 0;

    private void Start()
    {
        startButtton.onClick.AddListener(StartGame);
        stopButtton.onClick.AddListener(StopGame);
        StopGame();
        
    }

    public void ChangeLevelText(int level)
    {
        this.level.text = "Level: " + level.ToString();
        StartCoroutine(ShowLevel());
    }

    public void ChangeBackground()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex >= backgrounds.Length)
        {
            currentSpriteIndex = 0;
        }
        background.material.mainTexture = backgrounds[currentSpriteIndex].texture;
    }

    public void SetBeground(Sprite[] backgrounds)
    {
        this.backgrounds = backgrounds;
    }
    public void IncriseScore(int shapeData)
    {
        score.text = (int.Parse(score.text) + shapeData).ToString();
        chackScore?.Invoke();
    }

    public int GetScore()
    {
        return int.Parse(score.text);
    }

    public void StartGame()
    {
        shapeDestroyer.destroyShape += IncriseScore;
        startMenu?.Invoke();
        PlayMenu.SetActive(true);
        MainMenu.SetActive(false);
        background.gameObject.SetActive(true);
        ChangeLevelText(1);
        
        ChangeBackground();
    }

    public void StopGame()
    {
        shapeDestroyer.destroyShape -= IncriseScore;
        stopMenu?.Invoke();
        PlayMenu.SetActive(false);
        MainMenu.SetActive(true);
        background.gameObject.SetActive(false);
        level.gameObject.SetActive(false);
        ResetLevel();
    }

    IEnumerator ShowLevel()
    {
        level.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        level.gameObject.SetActive(false);
    }

        private void Update()
    {
        if (PlayMenu.active)
        {
            elapsedTime += Time.deltaTime;
            timer.text = "Time: " + elapsedTime.ToString("F2");
        }
    }

    public void ResetLevel()
    {
        score.text = "0";
        elapsedTime = 0;
        currentSpriteIndex = 0;
    }
}
