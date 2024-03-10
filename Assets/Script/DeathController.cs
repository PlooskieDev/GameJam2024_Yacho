using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private MovementController movementController;
    [SerializeField] private CameraControl cameracontroller;
    [SerializeField] private AudioSource deathSound;

    public void PlayerDied()
    {
        deathPanel.SetActive(true);
        movementController.canMove = false;
        cameracontroller.canRotate = false;
        deathSound.Play();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Helpers.MenuSceneName);
    }

}//END