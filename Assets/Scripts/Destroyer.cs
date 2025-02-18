using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
