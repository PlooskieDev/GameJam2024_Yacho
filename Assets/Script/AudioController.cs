using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    //Add comment to a script
    [TextArea(1, 5)]
    public string Notes = "Comment";

    //--------------------------------------------------------------------------------------------------------------------------

    public AudioSource randomAS;
    public AudioSource ambientAS;
    public AudioSource playerAS;

    public WarpController warpController;
    public float randomSoundTimer = 30f;
    public Animator animator;

    [Header("CP")]
    public AudioClip CP_ambientSound;
    public AudioClip CP_engineAssembly;
    public List<AudioClip> CP_randomSounds;
    public List<AudioClip> CP_footSteps;

    [Header("SP")]
    public AudioClip SP_ambientSound;
    public AudioClip SP_engineAssembly;
    public List<AudioClip> SP_randomSounds;
    public List<AudioClip> SP_footSteps;

    private Coroutine running;

    private void Start() {
        InvokeRepeating("PlayRandomSound", 10f, randomSoundTimer);

        WarpController.OnRealityChange += ChangeAmbientSound;
    }

    private void Update() {
        if (animator.GetFloat("Speed") > 0 && !animator.GetBool("Jump") && running == null && !animator.GetBool("Warp")) {
            running = StartCoroutine(PlayRandomFootStepSound());
        }
    }

    private void PlayRandomSound() {
        randomAS.clip = GetRandomSound();
        randomAS.Play();
    }

    public AudioClip GetRandomSound() {
        if (warpController.IsSteamPunk()) {
            if (SP_randomSounds.Count == 0) return null;
            int randomIndex = Random.Range(0, SP_randomSounds.Count - 1);
            return SP_randomSounds[randomIndex];
        } else {
            if (CP_randomSounds.Count == 0) return null;
            int randomIndex = Random.Range(0, CP_randomSounds.Count - 1);
            return CP_randomSounds[randomIndex];
        }
    }

    public void ChangeAmbientSound() {
        ambientAS.clip = GetAmbientSound();
        ambientAS.Play();
    }

    public AudioClip GetAmbientSound() {
        if (warpController.IsSteamPunk()) {
            return SP_ambientSound;
        } else {
            return CP_ambientSound;
        }
    }

    private IEnumerator PlayRandomFootStepSound() {
        playerAS.clip = GetRandomFootStepSound();
        playerAS.Play();
        yield return new WaitForSeconds(.3f);
        running = null;
    }

    public AudioClip GetRandomFootStepSound() {
        if (warpController.IsSteamPunk()) {
            if (SP_footSteps.Count == 0) return null;
            int randomIndex = Random.Range(0, SP_footSteps.Count - 1);
            return SP_footSteps[randomIndex];
        } else {
            if (CP_footSteps.Count == 0) return null;
            int randomIndex = Random.Range(0, CP_footSteps.Count - 1);
            return CP_footSteps[randomIndex];
        }
    }

}//END