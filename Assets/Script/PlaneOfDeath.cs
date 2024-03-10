using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneOfDeath : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public DeathController deathController;

    private void OnCollisionEnter(Collision collision)
    {
        //deathController.PlayerDied();
        SceneManager.LoadScene(Helpers.MenuSceneName);
    }

}//END