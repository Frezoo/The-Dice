using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIView : MonoBehaviour
{
   
   [Header("Тектсы")]
   [SerializeField] private TextMeshProUGUI WinConditionText;
   [SerializeField] private TextMeshProUGUI DrawConditionText;
   [SerializeField] private TextMeshProUGUI DefeatConditionText;
   [SerializeField] private TextMeshProUGUI ScoreText;
   
   [Header("Инпут филды)")]
   [SerializeField] private TMP_InputField WinConditionInputField;
   [SerializeField] private TMP_InputField DrawConditionInputField;
   [SerializeField] private TMP_InputField DefeatConditionInputField;
   
   [SerializeField , Header("Канвас группа")] private CanvasGroup canvasGroup;
   
   [Header("Ссылки на компоненты")]
   [SerializeField] private GameRules gameRules;
   [SerializeField] private ScoreManager scoreManager;
   [SerializeField] private DiceThrow diceThrow;

   void Awake()
   {
      gameRules.ConditionCalculated += DrawConditionCalculated;
      scoreManager.ScoreChanged += DrawScore;
      
      diceThrow.StartGame += DisableCanvasGroupInteractable;
      scoreManager.AllDicesProcessed += EnableCanvasGroupInteractable;
   }

   void DrawConditionCalculated(int drawCondition,int winCondition, int defeatCondition)
   {
      WinConditionText.text = $"Очков для победы >= <color=#7EFF00>{winCondition}</color>";
      WinConditionInputField.text = winCondition.ToString();
      
      DefeatConditionText.text =  $"Очков для поражения <= <color=#FF0000>{defeatCondition}</color>";
      DefeatConditionInputField.text = defeatCondition.ToString();
      
      DrawConditionText.text =  $"Очков для ничьи = <color=#FFEB04>{drawCondition}</color>";
      DrawConditionInputField.text = drawCondition.ToString();
   }

   void DrawScore(int score,GameConditional gameConditional)
   {
      var colorCode = "";
      switch (gameConditional)
      {
         case GameConditional.Draw:
            colorCode = "#FFEB04";
            break;
         case GameConditional.Defeat:
            colorCode = "#FF0000";
            Debug.Log("Defeat");
            break;
         case GameConditional.Win:
            colorCode = "#7EFF00";
            break;
         default:
            Debug.Log("Интересный случай");
            colorCode = "FFFFFF";
            break;
      }
      
      ScoreText.text = $"Выпало очков <color={colorCode}>:{score}</color>";
   }

   private void OnDestroy()
   {
      gameRules.ConditionCalculated -= DrawConditionCalculated;
      scoreManager.ScoreChanged -= DrawScore;
      diceThrow.StartGame -= DisableCanvasGroupInteractable;
      scoreManager.AllDicesProcessed -= EnableCanvasGroupInteractable;
   }

   private void DisableCanvasGroupInteractable()
   {
      canvasGroup.interactable = false;
   }

   private void EnableCanvasGroupInteractable()
   {
      canvasGroup.interactable = true;
   }
}
