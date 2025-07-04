using UnityEngine;
using System.Collections; 

[RequireComponent(typeof(Rigidbody2D))]
public class Ametralladora : MonoBehaviour
{
    private class ComportamientoBala : MonoBehaviour
    {
       
        private readonly string[] tagsColision = { "SueloDestruible", "Enemigo", "Suelo" };

        void OnCollisionEnter2D(Collision2D collision)
        {
            
            bool chocadoConAlgoSolido = false;
            foreach (string tag in tagsColision)
            {
                if (collision.gameObject.CompareTag(tag))
                {
                    chocadoConAlgoSolido = true;
                    break; 
                }
            }

            if (chocadoConAlgoSolido)
            {
                

                Destroy(gameObject); 
            }
            
        }

        
    }
 


    [Header("Referencias")]
    public Transform firePoint;         
    public GameObject bulletPrefab;      
    public Transform groundCheck;       

    [Header("Parámetros del Arma")]
    public float velocidadDeDisparo = 0.5f; 
    public float tiempoDeVidaBala = 1.5f;  
    public int disparosMaximosAire = 15; 
    public float fuerzaEmpujeDisparo = 3f;

    [Header("Parámetros de Ráfaga")]
    public int proyectilesPorRafaga = 3;  
    public float retrasoEntreBalasRafaga = 0.08f; 

    [Header("Parámetros de la Bala")]
    public float velocidadBala = 12f; 

    [Header("Chequeo de Suelo")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    private Rigidbody2D playerRb;
    private int balasRestantesEnAire;
    private bool isGrounded;
    private bool puedeDisparar = true; 
    private bool tocandoSueloPrevio = true; 

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("El script Ametralladora debe estar adjunto a un GameObject con un Rigidbody2D.", this);
            enabled = false;
            return;
        }

        if (firePoint == null || bulletPrefab == null || groundCheck == null)
        {
            Debug.LogError("¡Asigna Fire Point, Bullet Prefab y Ground Check en el Inspector!", this);
            enabled = false;
            return;
        }
        if (bulletPrefab.GetComponent<Collider2D>() == null || bulletPrefab.GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogError("¡El 'bulletPrefab' DEBE tener componentes Collider2D y Rigidbody2D!", this);
            enabled = false;
            return;
        }


        balasRestantesEnAire = disparosMaximosAire;
        ActualizarContadorBalas(true);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            if (!tocandoSueloPrevio)
            {
                Debug.Log("¡Tocado el suelo!");
                if (balasRestantesEnAire < disparosMaximosAire)
                {
                    RecargarBalas();
                }
                tocandoSueloPrevio = true;
            }
        }
        else
        {
            tocandoSueloPrevio = false;
        }

        if (Input.GetButtonDown("Fire1") && puedeDisparar && balasRestantesEnAire > 0)
        {
            StartCoroutine(DispararRafaga());
        }
        else if (Input.GetButtonDown("Fire1") && balasRestantesEnAire <= 0)
        {
            Debug.Log("¡Ametralladora sin balas en el aire!");
        }
    }

    IEnumerator DispararRafaga()
    {
        puedeDisparar = false;

        for (int i = 0; i < proyectilesPorRafaga; i++)
        {
            if (balasRestantesEnAire <= 0)
            {
                Debug.Log("¡Ametralladora sin balas en el aire durante ráfaga!");
                break;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>(); 

           
            rbBullet.velocity = -firePoint.up * velocidadBala;

           
            bullet.AddComponent<ComportamientoBala>();
            bullet.tag = "Bala"; 
            if (!isGrounded && playerRb != null)
            {
                playerRb.AddForce(firePoint.up * fuerzaEmpujeDisparo, ForceMode2D.Impulse);
            }

            balasRestantesEnAire--;
            ActualizarContadorBalas(false);
            Destroy(bullet, tiempoDeVidaBala);
          

            if (i < proyectilesPorRafaga - 1)
            {
                yield return new WaitForSeconds(retrasoEntreBalasRafaga);
            }
        }

        yield return new WaitForSeconds(velocidadDeDisparo);
        puedeDisparar = true;
    }

    void RecargarBalas()
    {
        if (balasRestantesEnAire < disparosMaximosAire)
        {
            balasRestantesEnAire = disparosMaximosAire;
            Debug.Log("¡Ametralladora Recargada!");
            ActualizarContadorBalas(true);
        }
    }

    void ActualizarContadorBalas(bool recargaCompleta)
    {
        if (!recargaCompleta)
        {
            Debug.Log("Balas restantes (Ametralladora): " + balasRestantesEnAire);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        puedeDisparar = true;
    }

    private void OnEnable()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            tocandoSueloPrevio = true;
            RecargarBalas();
        }
        else
        {
            tocandoSueloPrevio = false;
            ActualizarContadorBalas(false);
        }
        puedeDisparar = true;
    }
}