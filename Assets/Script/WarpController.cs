using UnityEngine;

public class WarpController : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public float distance = 100f;

    private enum Reality { CYBER_PUNK, STEAM_PUNK }
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
        transform.position += direction * distance;
        this.reality = reality;
    }

}//END