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
        public Animator animator;

        private bool canMove = true;
        private Rigidbody rb;
        private bool isGrounded = false;
        private float totalDistance;
        private Coroutine jump;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            if (canMove) {
                Move();
                if (isGrounded && Input.GetKeyDown(KeyCode.Space) && jump is null) {
                    jump = StartCoroutine(Jump());
                }
            }
        }

        private void Move() {
            float distanceThisFrame = rb.velocity.magnitude * moveSpeed * Time.deltaTime;
            totalDistance += distanceThisFrame;
            if (!isGrounded) return;

            Vector3 movementDirection = transform.forward * Input.GetAxis("Vertical") +
                                        transform.right * Input.GetAxis("Horizontal");

            rb.velocity = movementDirection * moveSpeed;
            animator.SetFloat("Speed", movementDirection.magnitude);

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

        private IEnumerator Jump() {
            animator.SetBool("Jump", true);
            yield return new WaitForSeconds(.3f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jump = null;
        }

        void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag(GameTags.GROUND)) {
                isGrounded = true;
                animator.SetBool("Jump", false);
            }
        }

    }
}//END