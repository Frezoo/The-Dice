using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TopFaceReader topFaceReader;
    private int score;
    
    private void Awake()
    {
        topFaceReader.OnTopFaceRead += AddScore;
    }

    public void  AddScore(int value)
    {
        score += value;
        Debug.Log($"Счет изменился, и составляет {score}");
    }

    private void OnDestroy()
    {
        topFaceReader.OnTopFaceRead -= AddScore;
    }
}
