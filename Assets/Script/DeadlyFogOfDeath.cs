using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyFogOfDeath : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private float speed = 10;
    
    void Update()
    {
        this.transform.position += direction * Time.deltaTime * speed / 1000;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(GameTags.PLAYER))
        {
            SceneManager.LoadScene(Helpers.MenuSceneName);
        }
    }
}
