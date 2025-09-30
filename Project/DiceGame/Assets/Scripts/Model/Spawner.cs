using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [Header("Префаб")]
    [SerializeField] private GameObject dicePrefab;

    [Header("Точки спавна")]
    [SerializeField] private List<Transform> spawnPoints = new();
    
    [Header("Ссылки на компоненты")]
    [SerializeField] private DiceThrow diceThrow;
    [SerializeField] private UIController uiController;
    [SerializeField] private ScoreManager scoreManager;
    
    private List<GameObject> dicePool = new();
    private GameConditional gameConditional;
    
    public event Action<int> DicesSpawned;


    void Awake()
    {
        uiController.NumberOfDiceChanged += SpawnDicesChanged;
        diceThrow.AddDiceInitTransforms(spawnPoints);
    }

    void SpawnDicesChanged(int countOfDice)
    {
        if (diceThrow == null)
        {
            Debug.Log($"Отсутсвует diceThrow чет");
            return;
        }

        DicesSpawned?.Invoke(countOfDice);

        if (dicePool.Count < countOfDice)
        {
            for (int i = dicePool.Count; i < countOfDice; i++)
            {
                var dice = Instantiate(dicePrefab, spawnPoints[i].position, Quaternion.identity);
                dicePool.Add(dice);
            }
        }
        else if (countOfDice >= 1 && dicePool.Count > countOfDice)
        {
            var toRemove = dicePool.Count - countOfDice;
            for (int i = 0; i < toRemove; i++)
            {
                var lastIndex = dicePool.Count - 1;
                Destroy(dicePool[lastIndex]);
                dicePool.RemoveAt(lastIndex);
            }
        }

        for (int i = 0; i < dicePool.Count; i++)
        {
            dicePool[i].transform.position = spawnPoints[i].position;
            dicePool[i].transform.rotation = spawnPoints[i].rotation;
            dicePool[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        

        diceThrow.AddDiceList(dicePool);
        scoreManager.SetDicesCount(countOfDice);
    }

    void OnDestroy()
    {
        uiController.NumberOfDiceChanged -= SpawnDicesChanged;
        
    }
}