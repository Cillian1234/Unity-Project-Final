using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject apple;
    public SnakeController snake;

    private int xRange;
    private int yRange;
    public Vector3 fruitPosition;
    public int fruitEaten;

    private bool isValidPosition;

    private void Start()
    {
        apple = GameObject.FindGameObjectWithTag("Food");
        fruitPosition = new Vector3(0, 3, -1.5f);
        fruitEaten = 0;
    }

    public void SpawnFood()
    {
        fruitEaten++;
        isValidPosition = true;

        int attempts = 0;
        do
        {

            xRange = Random.Range(-5, 6);
            yRange = Random.Range(-5, 6);
            fruitPosition = new Vector3(xRange, yRange, -1.5f);

            if (snake.getHeadPos() == fruitPosition)
            {
                isValidPosition = false;
                continue;
            }

            for (int i = 0; i < snake.bodyPositions.Count; i++)
            {
                if (snake.bodyPositions[i] == fruitPosition)
                {
                    isValidPosition = false;
                    break;
                }
            }

            attempts++;
            if (attempts >= 8000)
            {
                Debug.Log("Failed");
                break;
            }
        } while (!isValidPosition);

        apple.transform.position = fruitPosition;
    }


}
