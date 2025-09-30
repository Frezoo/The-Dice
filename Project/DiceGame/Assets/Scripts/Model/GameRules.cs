using System;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private Spawner spawner;
    [SerializeField] private TopFaceReader topFaceReader;
    [SerializeField] private UIController uiController;
    
    private int defeatScore;
    private int drawScore;
    private int winScore;

    public int DefeatScore
    {
        get { return defeatScore; }
    }

    public int DrawScore
    {
        get { return drawScore; }
    }

    public int WinScore
    {
        get { return winScore; }
    }
    
    public event Action<int,int,int> ConditionCalculated;
    
    void Awake()
    {
        spawner.DicesSpawned += CalculateGameConditions;
        uiController.WinConditionChanged += SetWinScore;
        uiController.DefeatConditionChanged += SetDefeatScore;
        uiController.DrawConditionChanged += SetDrawScore;
        
    }

    void CalculateGameConditions(int diceCounts)
    {
        var maxScore = diceCounts * 6;
        winScore = maxScore / 2 +1;
        defeatScore = maxScore / 2 - 1;
        drawScore = maxScore / 2;
        ConditionCalculated?.Invoke(drawScore,winScore,defeatScore);
    }

    void SetWinScore(int value)
    {
        winScore = value;
        ConditionCalculated?.Invoke(drawScore,winScore,defeatScore);
    }

    void SetDefeatScore(int value)
    {
        defeatScore = value;
        ConditionCalculated?.Invoke(drawScore,winScore,defeatScore);
    }
    
    void SetDrawScore(int value)
    {
        drawScore = value;
        ConditionCalculated?.Invoke(drawScore,winScore,defeatScore);
    }

    void OnDestroy()
    {
        spawner.DicesSpawned -= CalculateGameConditions;
        uiController.WinConditionChanged -= SetWinScore;
        uiController.DefeatConditionChanged -= SetDefeatScore;
        uiController.DrawConditionChanged -= SetDrawScore;

    }
    
}
