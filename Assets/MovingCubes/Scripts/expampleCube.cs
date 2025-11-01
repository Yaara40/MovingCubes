using UnityEngine;

public class expampleCube : MonoBehaviour
{
    public int direction = 1;
    
    void OnMouseDown()
    {
        Debug.Log("Mouse clicked!");
    }
    
    public void ChangeDirection()
    {
        int randomDirection = Random.Range(1, 7);
        while (randomDirection == direction)
        {
            randomDirection = Random.Range(1, 7);
        }
        direction = randomDirection;
        Debug.Log("Direction changed to: " + direction);
    }
    
    void Update()
    {
        if(direction == 1) transform.Translate(Vector3.left * Time.deltaTime * 5f);
        else if(direction == 2) transform.Translate(Vector3.right * Time.deltaTime * 5f);
        else if(direction == 3) transform.Translate(Vector3.back * Time.deltaTime * 5f);
        else if(direction == 4) transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        else if(direction == 5) transform.Translate(Vector3.down * Time.deltaTime * 5f);
        else if(direction == 6) transform.Translate(Vector3.up * Time.deltaTime * 5f);
    }
}