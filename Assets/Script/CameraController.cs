using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public float rotationSpeed = 100f;
    private bool isRotating = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating) {
            StartCoroutine(RotateObject(90));
        } else if (Input.GetKeyDown(KeyCode.E) && !isRotating) {
            StartCoroutine(RotateObject(-90));
        }
    }

    IEnumerator RotateObject(float angle) {
        isRotating = true;

        float totalRotation = 0f;
        while (totalRotation < Mathf.Abs(angle)) {
            float rotationAmount = Mathf.Min(rotationSpeed * Time.deltaTime, Mathf.Abs(angle) - totalRotation);
            transform.Rotate(Vector3.up, angle > 0 ? rotationAmount : -rotationAmount);
            totalRotation += Mathf.Abs(rotationAmount);
            yield return null;
        }

        isRotating = false;
    }

}//END