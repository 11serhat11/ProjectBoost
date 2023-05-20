using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionController : MonoBehaviour
{
    
    
    AudioSource audioSource;
    [SerializeField] AudioClip deathSound, successSound;
    [SerializeField] ParticleSystem deathPart, successPart;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update() 
    {
        Cheating();
    }

    void Cheating()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SuccessLanding();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    void OnCollisionEnter(Collision other) {
        
        if (isTransitioning || collisionDisabled) {return;}
        switch (other.gameObject.tag)
        {
            
            case "Friendly":
            Debug.Log("Friendly Collide");
            break;

            case "Finish":
            NextLevel();
            break;

            default:
            Restart();
            break;
        }
    }

    void Restart()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(deathSound);
        deathPart.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("RocketCrash", 1f);
    }
    
    void NextLevel()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(successSound);
        successPart.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("SuccessLanding", 1f);
    }
    
    void RocketCrash()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }
    
    void SuccessLanding()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        if (nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        }
        SceneManager.LoadScene(nextIndex);
    }
}
