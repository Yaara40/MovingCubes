using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER HIT BY: " + other.gameObject.name);
        
        expampleCube cube = other.GetComponent<expampleCube>();
        if (cube != null)
        {
            cube.ChangeDirection();
        }
    }
}