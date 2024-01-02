using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject pauseMenu;
    public GameObject Die;
    public float hp;
    public float hp2;
    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Die.SetActive(false);

    }

    void Update()
    {
        KlayController targetScript = FindObjectOfType<KlayController>();
        hp = targetScript.hp;
        BennoController targetScript2 = FindObjectOfType<BennoController>();
        hp2 = targetScript2.hp;
        if (hp <= 0 || hp2 <= 0)
        {
            StartCoroutine(WaitForAnimation());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

        }
        else
        {
            Time.timeScale = 1f; // 恢复游戏
            pauseMenu.SetActive(false); // 隐藏暂停菜单

        }
    }
    public void DiePause()
    {
        Time.timeScale = 0f;
        Die.SetActive(true);
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        DiePause();
    }
    public void GameStart()
    {
        print("game start");
        SceneManager.LoadScene("FinalScene");
    }
    public void Exit()
    {
        SceneManager.LoadScene("title");
    }
}