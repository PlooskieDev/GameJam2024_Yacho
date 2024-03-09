using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class MenuController : MonoBehaviour
    {
        [TextArea(1, 5)]
        public string Notes = "A controller that handles the button clicks";

        //--------------------------------------------------------------------------------------------------------------------------

        public void Play()
        {
            SceneManager.LoadScene(Helpers.GameSceneName);
        }

        public void Settings()
        {
            throw new NotImplementedException();
            //Lol to je na hrníček
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}//END