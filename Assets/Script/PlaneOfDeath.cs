using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneOfDeath : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public DeathController deathController;

    private void OnTriggerEnter(Collider other)
    {
        deathController.PlayerDied();
    }

}//END