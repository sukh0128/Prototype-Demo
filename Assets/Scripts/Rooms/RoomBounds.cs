using UnityEngine;

public class RoomBounds : MonoBehaviour
{
    private BoxCollider2D roomCollider;

    private void Awake()
    {
        roomCollider = GetComponent<BoxCollider2D>();
    }

    public Bounds GetBounds()
    {
        return roomCollider.bounds;
    }
}
