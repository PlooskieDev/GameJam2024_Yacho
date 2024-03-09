using UnityEngine;

public class AnimationController : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    private Animator animator;
    private CharacterController characterController;

    void Start()
    {
        // Get references to Animator and CharacterController components
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the character is moving
        bool isMoving = characterController.velocity.magnitude > 0.1f;

        // Update the Animator parameter to trigger the walk animation
        animator.SetBool("IsWalking", isMoving);
    }

}//END