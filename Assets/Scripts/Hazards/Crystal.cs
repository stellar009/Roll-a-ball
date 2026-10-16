using UnityEngine;

public class Crystal : MonoBehaviour
{
    // Called when another collider enters this object's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the Player
        if(other.CompareTag("Player"))
        {
            // Trigger game over when player touches the crystal
            GameManager.instance.GameOver();
        }
    }
}