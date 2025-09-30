using UnityEngine;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private BoxCollider[] diceFacesColliders;

    public void EnableFaceColliders()
    {
        foreach (BoxCollider diceFacesCollider in diceFacesColliders)
        {
            diceFacesCollider.enabled = true;
        }
    }
}