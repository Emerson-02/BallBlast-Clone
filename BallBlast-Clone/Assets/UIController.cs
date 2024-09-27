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

    private void Awake() {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoins(playerStats.coins);
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

    void NextLevel()
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

    void RestartLevel()
    {
        // Recarregar a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void BackMainMenu()
    {
        // Carregar a cena do menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
