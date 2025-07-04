using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;



public class Counter : MonoBehaviour
{
    public int Bidon;
    public int Gearbox;

    public TextMeshProUGUI BidonText;
    public TextMeshProUGUI GearBoxText;

    // Start is called before the first frame update
    void Start()
    {
        Counter_sum();
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GearBox"))
        {
            Destroy(other.gameObject);

            Bidon++;

            Counter_sum();
        }

        if (other.gameObject.CompareTag("Bidon"))
        {
            Destroy(other.gameObject);

            Gearbox++;

            Counter_sum();
        }
    }
    void Counter_sum()
    {
        BidonText.text = Bidon.ToString();
        GearBoxText.text = Gearbox.ToString();
    }
}
