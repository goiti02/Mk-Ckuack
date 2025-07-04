using UnityEngine;

public class BonusShip_SpaceInvaders : MonoBehaviour
{
    public float speed = 3f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet")) 
        {
            SoundManager_SpaceInvaders.Instance.Play(SoundType.Explosion);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}