using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private TopFaceReader topFaceReader;
    [SerializeField] private GameRules gameRules;
    [SerializeField] private DiceThrow diceThrow;
    
    private int score;
    private int dicesCount;
    private int processedDices;
    private string colorCode = "";
    
    private const string WinColorCode = "#7EFF00";
    private const string DrawColorCode = "#FFEB04";
    private const string DefeatColorCode = "#FF0000";
    
    private GameConditional currentGameConditional = GameConditional.Defeat;
    
    public event Action<int,String> ScoreChanged;
    public event Action AllDicesProcessed;
    private void Awake()
    {
        topFaceReader.OnTopFaceRead += AddScore;
        diceThrow.StartGame += ClearScore;
    }

    public void AddScore(int value)
    {
        score += value;
        processedDices++;
        SetGameConditional();
        SetScoreColor();
        ScoreChanged?.Invoke(score, colorCode);

        if (processedDices >= dicesCount)
            AllDicesProcessed?.Invoke();
    }

    private void SetGameConditional()
    {
        if (score >= gameRules.WinScore)
        {
            currentGameConditional = GameConditional.Win;
        }
        else if (score == gameRules.DrawScore)
        {
            currentGameConditional = GameConditional.Draw;
        }
        else
        {
            currentGameConditional = GameConditional.Defeat;
        }
    }

    void ClearScore()
    {
        score = 0;
        processedDices = 0;
        SetGameConditional();
        SetScoreColor();
        ScoreChanged?.Invoke(score, colorCode);
    }

    void SetScoreColor()
    {
        switch (currentGameConditional)
        {
            case GameConditional.Draw:
                colorCode = DrawColorCode;
                break;
            case GameConditional.Defeat:
                colorCode = DefeatColorCode;
                Debug.Log("Defeat");
                break;
            case GameConditional.Win:
                colorCode = WinColorCode;
                break;
            default:
                Debug.Log("Интересный случай");
                colorCode = "FFFFFF";
                break;
        }
    }
    
    public void SetDicesCount(int count)
    {
        dicesCount = count;
    }
    
    private void OnDestroy()
    {
        topFaceReader.OnTopFaceRead -= AddScore;
    }
}
