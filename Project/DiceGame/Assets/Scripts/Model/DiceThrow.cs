using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class DiceThrow : MonoBehaviour
{
    [Header("Бросок")] 
    [Range(0, 10)] [SerializeField] private float maxDeltaX;
    [Range(0, 10)] [SerializeField] private float maxDeltaY;
    [Range(0, 10)] [SerializeField] private float maxDeltaZ;

    [Header("Вращение")] 
    [Range(0, 10)] [SerializeField] private float maxTorqueX;
    [Range(0, 10)] [SerializeField] private float maxTorqueY;
    [Range(0, 10)] [SerializeField] private float maxTorqueZ;
    
    [Header("Ссылки на компоненты")]
    [SerializeField] private UIController uiController;
    
    private List<GameObject> dices = new List<GameObject>();
    private List<Transform> diceInitTransforms = new ();

    public event Action StartGame;


    void Awake()
    {
        uiController.ThrowButtonClicked += ThrowButtonClickedDice;
    }
    
    public void ThrowDiceByInputSystem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ThrowButtonClickedDice();
        }
    }

    private void ThrowButtonClickedDice()
    {
        StartGame?.Invoke();
        for (int i = 0; i < dices.Count; i++)
        {
            Rigidbody rb;
            if (dices[i].TryGetComponent<Rigidbody>(out rb))
            {
                rb.isKinematic = false;
                var throwVector = new Vector3(Random.Range(-maxDeltaX, maxDeltaX), Random.Range(0, maxDeltaY),
                    Random.Range(-maxDeltaZ, maxDeltaZ));
                var torqueVector = new Vector3(Random.Range(0, maxTorqueX), Random.Range(0, maxTorqueY),
                    Random.Range(0, maxTorqueZ));
                    
                dices[i].transform.position = diceInitTransforms[i].position;
                dices[i].GetComponent<DiceManager>().EnableFaceColliders();
                    
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                    
                rb.AddTorque(torqueVector, ForceMode.Impulse);
                rb.AddForce(throwVector, ForceMode.Impulse);
            }
            else
            {
                Debug.Log($"У Кубика? {dices[i].name} отсутсвует Rigibody");
            }

        }
    }


    public void AddDiceList(List<GameObject> diceList)
    {
        dices = diceList;
    }

    public void AddDiceInitTransforms(List<Transform> diceInitTransforms)
    {
        this.diceInitTransforms = diceInitTransforms;
    }

    private void OnDestroy()
    {
        uiController.ThrowButtonClicked -= ThrowButtonClickedDice;
    }
}