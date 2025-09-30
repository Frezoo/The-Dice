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
    
    private GameConditional currentGameConditional = GameConditional.Defeat;
    
    public event Action<int,GameConditional> ScoreChanged;
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
        ScoreChanged?.Invoke(score, currentGameConditional);

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
        ScoreChanged?.Invoke(score, currentGameConditional);
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
