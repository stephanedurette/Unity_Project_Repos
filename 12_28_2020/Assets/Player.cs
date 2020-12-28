using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float rollSpeed;
    [SerializeField] float mouseSens;
    [SerializeField] Transform cameraRig;

    Rigidbody rB;
    float rotationDegrees;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        rotationDegrees = 0f;
    }

    private void FixedUpdate()
    {
        UpdatePlayerRotation();
        UpdatePlayerMove();
        UpdateCameraRigTransform();
    }
    void UpdatePlayerRotation()
    {
        float mouseMove = Input.GetAxis("Mouse X");
        rotationDegrees += mouseMove * mouseSens * Time.deltaTime;
    }

    void UpdateCameraRigTransform() {
        cameraRig.position = transform.position;
        cameraRig.rotation = Quaternion.Lerp(cameraRig.rotation, Quaternion.Euler(0, rotationDegrees, 0), Time.time * .02f);
    }

    void UpdatePlayerMove() {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rB.velocity += Quaternion.AngleAxis(rotationDegrees, Vector3.up) * new Vector3(inputDir.x, 0, inputDir.y) * rollSpeed * Time.fixedDeltaTime;
    }
}
