using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public float rotationSpeed = 100f;
    public float movementSpeed = 5f;

    private Rigidbody rb;
    private bool isRotating = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating) {
            StartCoroutine(RotateObject(90));
        } else if (Input.GetKeyDown(KeyCode.E) && !isRotating) {
            StartCoroutine(RotateObject(-90));
        }
    }

    IEnumerator RotateObject(float angle) {
        isRotating = true;

        Quaternion targetRotation = rb.rotation * Quaternion.Euler(0, angle, 0);
        Quaternion startRotation = rb.rotation;

        float totalRotation = 0f;
        while (totalRotation < Mathf.Abs(angle)) {
            float remainingRotation = Mathf.Abs(angle) - totalRotation;
            float rotationAmount = Mathf.Min(rotationSpeed * Time.deltaTime, remainingRotation);
            Quaternion lerpedRotation = Quaternion.Lerp(startRotation, targetRotation, totalRotation / Mathf.Abs(angle));
            rb.MoveRotation(lerpedRotation);

            totalRotation += Mathf.Abs(rotationAmount);
            yield return null;
        }

        rb.MoveRotation(targetRotation);

        isRotating = false;
    }

}//END