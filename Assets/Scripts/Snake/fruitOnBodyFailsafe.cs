using UnityEngine;

public class fruitOnBodyFailsafe : MonoBehaviour
{
    public SpawnManager spawnManager;

    /*
     * My checks in other scripts to not spawn fruit on the snake worked about half the time and I don't know why
     * Easily 12 hours of looking into it and I couldn't figure it out
     * so I decided to just eat the fruit if it spawned on the body, this script removes the score it would've added and spawns more
     * really bolted on solution but hey, it works and I'm sick of this
     */

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.fruitEaten--;
        spawnManager.SpawnFood();
    }
}
