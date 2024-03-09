using Script.Facilities;
using Script.Utils;
using UnityEngine;

namespace Script
{
    public class PickupController : MonoBehaviour
    {
        //Add comment to a script
        [TextArea(1, 5)] public string Notes = "Comment";

        //--------------------------------------------------------------------------------------------------------------------------

        public GameObject hand;

        private ObjectState state;
        public bool hasFullHands = false;
        [HideInInspector] public GameObject pickupItem;
        private EngineAssembly engine;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                switch (state)
                {
                    case ObjectState.RELEASE:
                    {
                        ReleaseItem();
                    }
                        break;
                    case ObjectState.PICKUP:
                    {
                        PickupItem();
                    }
                        break;
                    case ObjectState.PLACE:
                    {
                        PlaceItem();
                    }
                        break;
                    case ObjectState.START_ENGINE:
                    {
                        StartEngine();
                    }
                        break;
                }
            }
        }

        private void PickupItem()
        {
            pickupItem.transform.position = Vector3.Lerp(pickupItem.transform.position, hand.transform.position, 5);
            pickupItem.transform.parent = hand.transform;
            pickupItem.GetComponent<Rigidbody>().isKinematic = true;
            pickupItem.GetComponent<Outline>().OutlineWidth = 0;
            state = ObjectState.RELEASE;
            hasFullHands = true;
            Debug.Log($"Picking up item {pickupItem.name}");
        }

        private void ReleaseItem()
        {
            Debug.Log($"Releasing item {pickupItem.name}");
            pickupItem.transform.parent = null;
            pickupItem.GetComponent<Rigidbody>().isKinematic = false;
            pickupItem.GetComponent<Outline>().OutlineWidth = 0;
            pickupItem = null;
            hasFullHands = false;
            state = ObjectState.NULL;
        }

        private void PlaceItem()
        {
            if (!engine.PlaceObject(pickupItem))
            {
                CheckFullHands();
                return;
            }

            pickupItem.GetComponent<PickupObject>().isPickable = false;
            pickupItem.GetComponent<Outline>().OutlineWidth = 0;
            pickupItem.transform.parent = null;
            pickupItem = null;
            hasFullHands = false;
            state = ObjectState.START_ENGINE;
        }

        private void StartEngine()
        {
            Debug.Log("Starting engine");
            engine.StartEngine();
        }

        public void Highlight_On(GameObject item)
        {
            pickupItem = item;
            state = ObjectState.PICKUP;
            pickupItem.GetComponent<Outline>().OutlineWidth = 10;
        }

        public void Highlight_Off(GameObject item)
        {
            item.GetComponent<Outline>().OutlineWidth = 0;
            pickupItem = null;
            state = ObjectState.NULL;
        }

        public void SetState(EngineAssembly engine, ObjectState state)
        {
            this.state = state;
            this.engine = engine;
        }

        private void CheckFullHands()
        {
            if (hasFullHands)
                state = ObjectState.RELEASE;
            else
                state = ObjectState.NULL;
        }

    } //END
}