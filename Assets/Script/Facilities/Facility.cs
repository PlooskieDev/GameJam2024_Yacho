using System.Collections.Generic;
using Script.Utils;
using UnityEngine;

namespace Script.Facilities
{
    public abstract class Facility : MonoBehaviour
    {
        public List<GameObject> objectPlaces;
        
        [HideInInspector] public PickupController pickupController;
        
        public abstract bool Assemble(GameObject objectToPlace);
        public abstract void FacilityStart();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(GameTags.PLAYER))
            {
                pickupController = other.GetComponent<PickupController>();
                if (pickupController.hasFullHands && pickupController.pickupItem != null)
                    pickupController.SetState(this, ObjectState.PLACE);
                else if (!pickupController.hasFullHands)
                    pickupController.SetState(this, ObjectState.START_ENGINE);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(GameTags.PLAYER))
            {
                pickupController = other.GetComponent<PickupController>();
                if (pickupController.hasFullHands && pickupController.pickupItem != null)
                    pickupController.SetState(this, ObjectState.RELEASE);
                else if (!pickupController.hasFullHands)
                    pickupController.SetState(this, ObjectState.NULL);
            }
        }
    }
}