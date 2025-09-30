using UnityEngine;
using UnityEngine.InputSystem;

public class RebindScript : MonoBehaviour
{
    
    public void StartRebinding(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var action = context.action.actionMap.FindAction("Throw");//Объективно не самый корректный способ получение ссылки на действие, но все осталные способы ссылались куда то не туда.
            if (action == null)
            {
                Debug.LogError("Нет действия для ребинда");
                return;
            }

            if (action.enabled)
                action.Disable();

            Debug.Log("Ожидание новой клавиши");

            action.PerformInteractiveRebinding()
                .OnComplete(rebindingOperation =>
                {
                    Debug.Log("Готово: " + action.bindings[0].effectivePath);
                    action.Enable();
                    rebindingOperation.Dispose();
                })
                .Start();
        }
    }
}
