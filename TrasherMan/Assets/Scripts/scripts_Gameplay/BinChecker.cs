using UnityEngine;

public class BinChecker : MonoBehaviour
{
    public TrashCategory acceptedCategory;

    private void OnCollisionEnter(Collision collision)
    {
        TrashType trash = collision.gameObject.GetComponent<TrashType>();

        if (trash != null && trash.category == acceptedCategory)
        {
            Debug.Log("Correct bin!");
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Wrong bin!");
        }
    }
}