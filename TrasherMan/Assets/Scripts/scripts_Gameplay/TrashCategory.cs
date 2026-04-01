using UnityEngine;

public enum TrashCategory
{
    Garbage,
    Recyclables,
    Compost,
    Electronics,
    Glass
}

public class TrashType : MonoBehaviour
{
    public TrashCategory category;
}