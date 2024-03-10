using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneOfDeath : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(GameTags.PLAYER))
        {
            SceneManager.LoadScene(Helpers.MenuSceneName);
        }
    }

}//END