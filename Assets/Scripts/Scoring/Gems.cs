using UnityEngine;

public class Gems : MonoBehaviour
{
    // Called when another collider enters this gem's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the Player
        if(other.CompareTag("Player"))
        {
            // Increase the gem counter in the GameManager
            GameManager.instance.Counter();

            // Hide the gem instead of destroying it (cheaper)
            gameObject.SetActive(false);
        }
    }
}