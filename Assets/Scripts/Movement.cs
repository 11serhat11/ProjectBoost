using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float mainSpeed = 1;
    [SerializeField] float rotationThrust = 1;
    [SerializeField] AudioClip flySound;
    [SerializeField] ParticleSystem leftPart, middlePart, rightPart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Thrusting();
        }
        else
        {
            StopThrusting();
        }
    }

        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotation();
        }

    }

    void Thrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(flySound);
        }

        rb.AddRelativeForce(Vector3.up * mainSpeed * Time.deltaTime); // same with AddRelativeForce(0,1,0)

        if (!middlePart.isPlaying)
        {
            middlePart.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        middlePart.Stop();
    }

    void StopRotation()
    {
        rightPart.Stop();
        leftPart.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!leftPart.isPlaying)
        {
            leftPart.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!rightPart.isPlaying)
        {
            rightPart.Play();
        }
    }

    void ApplyRotation(float rotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
