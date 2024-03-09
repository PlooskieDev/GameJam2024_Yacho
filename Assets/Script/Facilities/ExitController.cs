using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Facilities
{
    public class ExitController : Facility
    {
        private bool IsPowered = false;
        private bool IsAssembled = false;
        
        private void Start()
        {
            EngineAssembly.OnPowerOn += () => IsPowered = true;
        }
        public override bool Assemble(GameObject objectToPlace)
        {
            if (!objectPlaces.Any()) return false;
            var place = objectPlaces.First();
            if (place.name == objectToPlace.name)
            {
                objectToPlace.transform.position = place.transform.position;
                objectToPlace.transform.rotation = place.transform.rotation;
                IsAssembled = true;
                return true;
            }
            return false;
        }

        public override void FacilityStart()
        {
            if (!IsPowered) return;
            // Play sound
            if (!IsAssembled) return;
            // Ending animation + sound
            StartCoroutine(GoToMainMenu());
        }

        private IEnumerator GoToMainMenu()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(Helpers.MenuSceneName);
        }

    }
}