using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Gear_Movement : MonoBehaviour
{
    public float height; //altura
    public float speed;
    public float rotationSpeed;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        MovementGameObject();
    }

    void MovementGameObject()
    {
        float newfloatposition = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        //el Time.Time*Speed lo que hace es que la velocidad sea constante y no cambie
        //El time.Deltatime QUE NO ESTA lo que hace es hacer que la moneda tenga aceleración. 
        transform.position = new Vector3(startPosition.x, newfloatposition, startPosition.z);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }


}