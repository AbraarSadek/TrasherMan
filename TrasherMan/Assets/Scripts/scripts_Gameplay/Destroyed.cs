using UnityEngine;

public class Destroyed : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if both objects have the SAME tag
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log("Correct bin! Destroying: " + gameObject.name);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Wrong bin! This is " + gameObject.tag +
                      " but hit " + collision.gameObject.tag);
        }
    }
}