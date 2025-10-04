using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField numberOfDicesField;
    [SerializeField] private TMP_InputField winConditionInputField;
    [SerializeField] private TMP_InputField drawConditionInputField;
    [SerializeField] private TMP_InputField defeatConditionInputField;
    
    [SerializeField] private Button throwButton;
    
    public event Action<int> NumberOfDiceChanged;
    public event Action ThrowButtonClicked;
    
    public event Action<int> WinConditionChanged;
    public event Action<int> DrawConditionChanged;
    public event Action<int> DefeatConditionChanged;
    

    private void Awake()
    {
        numberOfDicesField.onValueChanged.AddListener(EnterDiceCount);
        
        winConditionInputField.onValueChanged.AddListener(ChangeWinConditional);
        drawConditionInputField.onValueChanged.AddListener(ChangeDrawConditional);
        defeatConditionInputField.onValueChanged.AddListener(ChangeDefeatConditional);
        
        
        throwButton.onClick.AddListener(MakeThrow);
    }

    private void EnterDiceCount(string count)
    {
        if (int.TryParse(count, out int value))
        {
            value = Mathf.Clamp(value, 0, 5);
            NumberOfDiceChanged?.Invoke(value);
            numberOfDicesField.text = value.ToString();
        }
    }

    private void ChangeWinConditional(string input)
    {
        if (int.TryParse(input, out int value))
        {
            WinConditionChanged?.Invoke(value);
        }
    }

    private void ChangeDrawConditional(string input)
    {
        if (int.TryParse(input, out int value))
        {
            DrawConditionChanged?.Invoke(value);
        }
    }
    
    private void ChangeDefeatConditional(string input)
    {
        if (int.TryParse(input, out int value))
        {
            DefeatConditionChanged?.Invoke(value);
        }
    }
    
    private void MakeThrow()
    {
        ThrowButtonClicked?.Invoke();
    }
    
}
