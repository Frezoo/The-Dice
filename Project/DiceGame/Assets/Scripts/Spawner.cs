using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [Range(1, 5)] [SerializeField] private int numberOfDice;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DiceThrow diceThrow;
    
    void Awake()
    {
        if (diceThrow == null)
        {
            Debug.Log($"Отсутсвует diceThrow чет");
            return;
        }
        for (int i = 0; i < numberOfDice; i++)
        {
            var dice = Instantiate(dicePrefab, spawnPoints[i].position, Quaternion.identity);
            diceThrow.AddDice(dice);
        }
    }
    
    
}