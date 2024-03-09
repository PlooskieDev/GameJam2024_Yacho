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
        public GameObject playerObject;

        private Rigidbody rb;
        private bool isGrounded = false;
        private bool isMoving = false;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            Move();
            Jump();
        }

        private void Move() {
            if (!isGrounded) return;
            Vector3 movementDirection = transform.forward * Input.GetAxis("Vertical") +
                                        transform.right * Input.GetAxis("Horizontal");

            rb.velocity = movementDirection * moveSpeed;
            if (movementDirection.magnitude > 0)
                playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * playerRotationSpeed);
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

    }
}//END