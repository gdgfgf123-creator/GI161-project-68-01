using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float DestroyDelay;

    private void Start()
    {
        Destroy(gameObject, DestroyDelay);
    }


}
