using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Referência para o texto de moedas
    public TextMeshProUGUI coinsText;
    public PlayerStats playerStats;
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject panelOptions;
    public GameObject panelMenu;

    private void Awake() {
        // se esse nível não for o menu
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        // se esse nível não for o menu
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            // Atualizar o texto de moedas
            UpdateCoins(playerStats.coins);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoins(int coins)
    {
        // Atualizar o texto de moedas
        coinsText.text = coins.ToString();
    }

    public void NextLevel()
    {
        // O nome da cena atual a partir de "-"
        string currentSceneName = SceneManager.GetActiveScene().name;
        string[] splitName = currentSceneName.Split('-');

        // O número da cena atual
        int currentLevel = int.Parse(splitName[1]);

        // O nome da próxima cena
        string nextSceneName = splitName[0] + "-" + (currentLevel + 1);

        // Carregar a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartLevel()
    {
        // Recarregar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMainMenu()
    {
        // Carregar a cena do menu principal
        SceneManager.LoadScene("Menu");
    }

    public void ShowWinPanel()
    {
        panelWin.SetActive(true);
    }

    public void ShowLosePanel()
    {
        panelLose.SetActive(true);
    }

    public void StartGame()
    {
        // Carregar a primeira cena
        SceneManager.LoadScene("Level-1");
    }

    void OpenOptions()
    {
        panelMenu.SetActive(false);
        panelOptions.SetActive(true);
    }

    void CloseOptions()
    {
        panelMenu.SetActive(true);
        panelOptions.SetActive(false);
    } 

    public void QuitGame()
    {
        #if UNITY_EDITOR
        // Se estiver no editor, pare a execução do jogo
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Se estiver em um build, feche o aplicativo
        Application.Quit();
        #endif
    }
}
