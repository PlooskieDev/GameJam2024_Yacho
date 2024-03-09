using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public Transform camera;
    public Transform player;

    public Material transparentWall, transparentBlack;
    public Material solidWall, solidBlack;
    public LayerMask wallLayer;

    public float radius = 2f;

    public List<GameObject> objectInTheWay = new();

    private void Start() {

    }

    private void Update() {
        GetObjects();
        MakeObjectsSolid();
    }

    private void GetObjects() {
        RaycastHit[] allHits = Hit();

        foreach (RaycastHit hit in allHits) {
            if (!objectInTheWay.Contains(hit.collider.gameObject)) {
                objectInTheWay.Add(hit.collider.gameObject);
                SetObjectTransparent(hit.collider.gameObject);
            }
        }
    }

    private void MakeObjectsSolid() {
        foreach (GameObject obj in objectInTheWay.ToArray()) {
            if (!IsBetweenCameraAndPlayer(obj)) {
                SetObjectSolid(obj);
                objectInTheWay.Remove(obj);
            }
        }
    }

    private bool IsBetweenCameraAndPlayer(GameObject obj) {
        RaycastHit[] hits = Hit();

        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject == obj) {
                return true;
            }
        }

        return false;

    }

    private void SetObjectTransparent(GameObject obj) {
        if (obj.TryGetComponent(out MeshRenderer mesh)) {
            mesh.materials = new Material[] { transparentBlack, transparentWall };
        }
    }

    private void SetObjectSolid(GameObject obj) {
        if (obj.TryGetComponent(out MeshRenderer mesh)) {
            mesh.materials = new Material[] { solidBlack, solidWall };
        }
    }

    private RaycastHit[] Hit() {
        Vector3 direction = (player.position - camera.position).normalized;
        float distance = Vector3.Distance(camera.position, player.position);

        return Physics.CapsuleCastAll(camera.position, player.position, radius, direction, distance, wallLayer);

    }

}//END