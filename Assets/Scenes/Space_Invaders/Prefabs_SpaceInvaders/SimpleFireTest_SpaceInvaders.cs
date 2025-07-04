using UnityEngine;

public class SimpleFireTest_SpaceInvaders : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
       
        if (Input.GetButtonDown("Fire"))
        {
            Debug.Log("¡Bala disparada! (Desde el script de prueba)");
            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Falta asignar el Prefab de la Bala o el FirePoint en el Inspector.");
            }
        }
    }
}