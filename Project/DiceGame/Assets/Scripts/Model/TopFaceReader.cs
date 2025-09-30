using System;
using UnityEngine;

public class TopFaceReader : MonoBehaviour
{
    public event Action<int> OnTopFaceRead;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent == null)
        {
            Debug.Log("Have not parent");
            return;
        }

        var parent = other.transform.parent;
        var rb = parent.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("Have not rb");
            return;
        }
        
        if (rb.linearVelocity.sqrMagnitude <= 0.00000001f && rb.angularVelocity.sqrMagnitude <= 0.00000001f) 
        {
            Debug.Log($"Dice {parent.name} упокоилась кароче");
            OnTopFaceRead?.Invoke(GetTopFaceView(other.gameObject));
            other.enabled = false;
        }

    }

    int GetTopFaceView(GameObject bottomSide)
    {
        var botoomFace = 0;
        
        if(int.TryParse(bottomSide.tag, out botoomFace))
            return 7 - botoomFace;
        Debug.Log("Что-то пошло нет так");
        return 0;
    }
}
