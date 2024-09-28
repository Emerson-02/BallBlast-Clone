using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Referência para o texto de moedas
    public TextMeshProUGUI coinsText;
    public PlayerStats playerStats;
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject panelOptions;
    public GameObject panelMenu;
    public Button btnUpgrade;
    public TextMeshProUGUI txtValueUpgrade;
    public int upgradeCountMax = 4;
    public int level = 1;


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

            txtValueUpgrade.text = playerStats.upgradePrice.ToString();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // se esse nível não for o menu
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            // se upgradeCount for igual a upgradeCountMax
            if (playerStats.upgradeCount == upgradeCountMax)
            {
                // desabilita o botão de upgrade
                btnUpgrade.interactable = false;

                // setActive do botão de upgrade para false
                btnUpgrade.gameObject.SetActive(false);
            }
        }
            
        
    }

    public void UpdateCoins(int coins)
    {
        // Atualizar o texto de moedas
        coinsText.text = coins.ToString();
    }

    public void OnUpgradeButtonClicked()
    {
        if (playerStats.coins >= playerStats.upgradePrice && playerStats.upgradeCount < upgradeCountMax)
        {
            // Subtrai o custo das moedas do jogador
            playerStats.UpdateCoins(playerStats.coins - playerStats.upgradePrice);

            // Atualiza o texto de moedas
            UpdateCoins(playerStats.coins);

            // Diminui em 0.1f o intervalo entre as balas
            playerStats.bulletInterval -= 0.1f;

            // Aumenta o custo do próximo upgrade
            playerStats.upgradePrice += 1;

            // Atualiza o texto do botão
            txtValueUpgrade.text = playerStats.upgradePrice.ToString();

            playerStats.upgradeCount++;

            // salva as estatísticas do jogador
            playerStats.SavePlayerStats();

            // Atualiza a interatividade do botão
            UpdateButtonInteractivity();
        }
    }

    public void UpdateButtonInteractivity()
    {
        // Se o jogador tiver moedas suficientes para o upgrade
        if (playerStats.coins >= playerStats.upgradePrice)
        {
            // Ativa o botão
            btnUpgrade.interactable = true;
        }
        else
        {
            // Desativa o botão
            btnUpgrade.interactable = false;
        }
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

        // aumenta o level do jogador
        level++;

        // Salvar o nível
        SaveLevel();

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
        // pega o level em que o jogador parou
        LoadLevel();

        // junta o nome da cena com o level
        string sceneName = "Level-" + level;

        // Carregar a primeira cena
        SceneManager.LoadScene(sceneName);
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

    // Método para salvar o nível
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", level);
        PlayerPrefs.Save();
        Debug.Log("Level saved: " + level);
    }

    // Método para carregar o nível
    public void LoadLevel()
    {
        if (PlayerPrefs.HasKey("PlayerLevel"))
        {
            level = PlayerPrefs.GetInt("PlayerLevel");
            Debug.Log("Level loaded: " + level);
        }
    }

    // Exemplo de método para atualizar o nível e salvar
    public void UpdateLevel(int newLevel)
    {
        level = newLevel;
        SaveLevel();
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
