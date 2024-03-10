using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Facilities {
    public class EngineAssembly : Facility {
        //Add comment to a script
        [TextArea(1, 5)] public string Notes = "Comment";

        //--------------------------------------------------------------------------------------------------------------------------

        public static UnityAction OnPowerOn;

        [Header("Audio")]
        public AudioSource engineRun1;
        public AudioSource engineRun2;
        public AudioSource engineAssemble;

        private Dictionary<Parts, EnginePart> parts = new Dictionary<Parts, EnginePart>();

        private void Start()
        {
            foreach (var place in objectPlaces)
            {
                parts.Add((Parts)System.Enum.Parse(typeof(Parts), place.name.Substring(2)),
                    new EnginePart()
                    {
                        Name = place.name,
                        Transform = place.transform,
                        IsPresent = false,
                    });
            }
        }

        public override bool Assemble(GameObject objectToPlace) {
            var place = parts.FirstOrDefault(part => part.Value.Name.Contains(objectToPlace.name)).Value;
            if (place != null) {
                objectToPlace.transform.position = place.Transform.position;
                objectToPlace.transform.rotation = place.Transform.rotation;
                place.IsPresent = true;
                engineAssemble.Play();
                return true;
            }

            return false;
        }

        public override void FacilityStart() {
            if (!parts.GetValueOrDefault(Parts.Button_place).IsPresent) {
                return;
            } else {
                Debug.Log("Button press");
            }

            if (!parts.GetValueOrDefault(Parts.Energy_place).IsPresent) {
                return;
            } else {
                Debug.Log("Power on!");
                OnPowerOn?.Invoke();
                PlayRunSound();
            }

            if (parts.GetValueOrDefault(Parts.Safety_place).IsPresent) {
                return;
            } else {
                Debug.Log("BUUUM!");
            }
        }

        private class EnginePart {
            public string Name { get; set; }
            public Transform Transform { get; set; }
            public bool IsPresent { get; set; }
            public Parts Part { get; set; }
        }

        private enum Parts {
            Button_place,
            Energy_place,
            Safety_place
        }

        private void PlayRunSound() {
            engineRun1.Play();
            engineRun2.Play();
        }

    } //END
}