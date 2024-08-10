using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject ResultPanel;
    [SerializeField] private AsteroidBehaviour enemyPrefab;
    [SerializeField] private GameObject[] ufoPrefab;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI chain;
    [SerializeField] private GameObject ctrlPanel;
    public int enemyCount = 0;
    private int level = 0;
    private int currentScore = 0;
    private bool isPaused = true;
    private Vector2 worldSpawnPosition;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResultPanel.gameObject.SetActive(false);
        TogglePause(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isPaused == true)
            {
                isPaused = false;
            }
            //else
            //{
            //    isPaused = true;
            //}
        }
        TogglePause(isPaused);
        if (enemyCount <= 0)
        {
            enemyCount = 0;
            level++;

            int numEnemies = 2 + (2 * level);
            for (int i = 0; i < numEnemies; i++)
            {
                SpawnEnemy();
            }
            if (level % 3 == 0)
            {
                int numUFOs = 1 + (level / 3);
                for (int i = 0; i < numUFOs; i++)
                {
                    SpawnUFO();
                }
            }
        }
    }
    private void SpawnEnemy()
    {
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0:
                viewportSpawnPosition = new Vector2(offset, 0);
                break;
            case 1:
                viewportSpawnPosition = new Vector2(offset, 1);
                break;
            case 2:
                viewportSpawnPosition = new Vector2(0, offset);
                break;
            case 3:
                viewportSpawnPosition = new Vector2(1, offset);
                break;
        }
        worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        AsteroidBehaviour enemy = Instantiate(enemyPrefab, worldSpawnPosition, Quaternion.identity);
    }
    private void SpawnUFO()
    {
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;
        int edge = Random.Range(0, 4);
        switch (edge)
        {
            case 0:
                viewportSpawnPosition = new Vector2(offset, 0);
                break;
            case 1:
                viewportSpawnPosition = new Vector2(offset, 1);
                break;
            case 2:
                viewportSpawnPosition = new Vector2(0, offset);
                break;
            case 3:
                viewportSpawnPosition = new Vector2(1, offset);
                break;
        }
        worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        GameObject ufo = Instantiate(ufoPrefab[0], worldSpawnPosition, Quaternion.identity);

        if (Random.Range(0f, 1f) > 0.5f)
        {
            GameObject ufo2 = Instantiate(ufoPrefab[1], worldSpawnPosition, Quaternion.identity);
        }
        if (Random.Range(0f, 1f) > 0.2f)
        {
            GameObject ufo2 = Instantiate(ufoPrefab[2], worldSpawnPosition, Quaternion.identity);
        }

    }
    public void GameOver()
    {
        HighScoreManager highScoreManager = FindObjectOfType<HighScoreManager>();

        highScoreManager.AddHighScore(new HighScoreElement(currentScore));
        ResultPanel.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
        ResultPanel.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = highScoreManager.getTopScore().ToString();
        TogglePause(true);
        ResultPanel.gameObject.SetActive(true);

    }

    public IEnumerator End()
    {
        Debug.Log("Game Over");
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
        yield return null;
    }
    public void AddScore(int tmpscore)
    {
        currentScore += tmpscore;
        score.text = currentScore.ToString();
    }
    public void showChain(int Chain)
    {
        chain.text = Chain.ToString();
    }
    void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            ctrlPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
