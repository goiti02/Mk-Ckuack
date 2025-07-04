using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;

    [Header("Infinite Scroll Settings")]
    public bool infiniteScroll = true;
    private float spriteWidth;

    private void Start()
    {
        if (infiniteScroll)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                spriteWidth = sr.bounds.size.x;
            }
        }
    }

    public void Move(float delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor;

        transform.localPosition = newPos;

        if (infiniteScroll)
        {
            // Reposicionar cuando se sale del rango
            if (Mathf.Abs(newPos.x) >= spriteWidth)
            {
                float offset = newPos.x % spriteWidth;
                transform.localPosition = new Vector3(offset, newPos.y, newPos.z);
            }
        }
    }
}
