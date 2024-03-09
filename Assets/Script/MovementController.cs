using System.Collections;
using UnityEngine;

namespace Script {
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour {
        //Add comment to a script
        [TextArea(1, 5)]
        public string Notes = "Comment";

        //--------------------------------------------------------------------------------------------------------------------------

        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public float playerRotationSpeed = 10f;
        public float maxDistance = 100f;
        public GameObject playerObject;
        public WarpController warpController;

        private bool canMove = true;
        private Rigidbody rb;
        private bool isGrounded = false;
        private float totalDistance;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            if (canMove) {
                Move();
                Jump();
            }
        }

        private void Move() {
            float distanceThisFrame = rb.velocity.magnitude * moveSpeed * Time.deltaTime;
            totalDistance += distanceThisFrame;
            if (!isGrounded) return;

            Vector3 movementDirection = transform.forward * Input.GetAxis("Vertical") +
                                        transform.right * Input.GetAxis("Horizontal");

            //float distanceThisFrame = movementDirection.magnitude * moveSpeed * Time.deltaTime;
            //totalDistance += distanceThisFrame;
            rb.velocity = movementDirection * moveSpeed;

            if (movementDirection.magnitude > 0) {
                playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * playerRotationSpeed);
            }

            if (totalDistance >= maxDistance) {
                canMove = false;
                totalDistance = 0f;
                StartCoroutine(Warp());
            }
        }

        private IEnumerator Warp() {
            Debug.Log("WARPING!!!");
            yield return new WaitForSeconds(2);
            Debug.Log("REALITY CHANGED!!!");
            warpController.Warp();
            canMove = true;
        }

        private void Jump() {
            if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag(GameTags.GROUND)) {
                isGrounded = true;
            }
        }

        private void DoSomething() {
            // Perform your action here after moving the specified distance
            Debug.Log("Player has moved " + 10 + " units.");
        }

    }
}//END