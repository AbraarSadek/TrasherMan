using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Garbage" || tag == "Recycling" || tag == "Compost")
        {
            Debug.Log("Destroyed " + gameObject.name + " after hitting " + tag + " bin");
            Destroy(gameObject);
        }
    }
}