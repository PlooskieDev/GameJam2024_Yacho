using UnityEngine;

public class PickubObject : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    private PickupController pickupController;

    public bool isPickable = true;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals(GameTags.PLAYER) && isPickable) {
            pickupController = other.GetComponent<PickupController>();
            if (!pickupController.hasFullHands && pickupController.pickupItem == null) {
                pickupController.Highlight_On(gameObject);
                Debug.Log($"Can pickup {gameObject}");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals(GameTags.PLAYER) && isPickable) {
            pickupController = other.GetComponent<PickupController>();
            if (!pickupController.hasFullHands && pickupController.pickupItem != null) {
                pickupController.Highlight_Off(gameObject);
                Debug.Log($"Cannot pickup {gameObject}");
            }
        }
    }

}//END