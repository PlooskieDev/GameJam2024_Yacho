using UnityEngine;

public class WarpController : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public float mapDistance = 100f;
    public float maxDistance = 100f;

    private Reality reality = Reality.STEAM_PUNK;

    public void Warp() {
        switch (reality) {
            case Reality.CYBER_PUNK:
                Transform(Vector3.left, Reality.STEAM_PUNK);
                break;
            case Reality.STEAM_PUNK:
                Transform(Vector3.right, Reality.CYBER_PUNK);
                break;
        }
    }

    private void Transform(Vector3 direction, Reality reality) {
        transform.position += direction * mapDistance;
        this.reality = reality;
    }

    public bool IsSteamPunk() {
        return reality.Equals(Reality.STEAM_PUNK);
    }

}//END

public enum Reality { CYBER_PUNK, STEAM_PUNK }