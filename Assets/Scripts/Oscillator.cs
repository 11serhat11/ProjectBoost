using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} //compared smallest floating num
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; //180' * 2 = 360' (circle)
        float rawSinWave = Mathf.Sin(tau * cycles); //sin(0,360) = [-1,1]
        movementFactor = (rawSinWave + 1) / 2; //[0,2] / 2 = [0,1]
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
