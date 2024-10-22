using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float bulletInterval = 0.5f; // Intervalo entre as balas em segundos
    public int bulletsPerShot = 1; // Número de balas disparadas por vez
    public float bulletSpeed = 10f; // Velocidade das balas
    public int damage = 1; // Dano causado pelas balas
    public int coins = 0; // Moedas coletadas
    public int upgradePrice = 3; // Preço do upgrade

    public int upgradeCount = 0;
    //public int level = 1;
    //public int upgradeCountMax = 4;

    public void Start()
    {
        //LoadPlayerStats();

        // Verifica se é a primeira vez que a cena "level-1" é aberta
        if (!PlayerPrefs.HasKey("Level1Initialized"))
        {
            // Define os valores padrão
            bulletInterval = 0.5f;
            bulletsPerShot = 1;
            bulletSpeed = 10f;
            damage = 1;
            coins = 0;
            upgradePrice = 3;
            upgradeCount = 0;

            // Salva os valores padrão
            SavePlayerStats();

            // Marca que a cena "level-1" foi inicializada
            PlayerPrefs.SetInt("Level1Initialized", 1);
            PlayerPrefs.Save();
        }
        else
        {
            // Carrega os valores salvos
            LoadPlayerStats();
        }
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("BulletInterval", bulletInterval);
        PlayerPrefs.SetInt("BulletsPerShot", bulletsPerShot);
        PlayerPrefs.SetFloat("BulletSpeed", bulletSpeed);
        PlayerPrefs.SetInt("Damage", damage);
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("UpgradePrice", upgradePrice);
        PlayerPrefs.SetInt("UpgradeCount", upgradeCount);
        PlayerPrefs.Save();
        Debug.Log("Player stats saved!");
    }

    public void LoadPlayerStats()
    {
        if (PlayerPrefs.HasKey("BulletInterval"))
        {
            bulletInterval = PlayerPrefs.GetFloat("BulletInterval");
        }

        if (PlayerPrefs.HasKey("BulletsPerShot"))
        {
            bulletsPerShot = PlayerPrefs.GetInt("BulletsPerShot");
        }

        if (PlayerPrefs.HasKey("BulletSpeed"))
        {
            bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        }

        if (PlayerPrefs.HasKey("Damage"))
        {
            damage = PlayerPrefs.GetInt("Damage");
        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        if (PlayerPrefs.HasKey("UpgradePrice"))
        {
            upgradePrice = PlayerPrefs.GetInt("UpgradePrice");
        }

        if (PlayerPrefs.HasKey("UpgradeCount"))
        {
            upgradeCount = PlayerPrefs.GetInt("UpgradeCount");
        }

        Debug.Log("Player stats loaded!");
    }
    public void UpdateBulletInterval(float newInterval)
    {
        bulletInterval = newInterval;
        SavePlayerStats();
    }

    public void UpdateBulletsPerShot(int newBulletsPerShot)
    {
        bulletsPerShot = newBulletsPerShot;
        SavePlayerStats();
    }

    public void UpdateBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
        SavePlayerStats();
    }

    public void UpdateDamage(int newDamage)
    {
        damage = newDamage;
        SavePlayerStats();
    }

    public void UpdateCoins(int newCoins)
    {
        coins = newCoins;
        SavePlayerStats();
    }

    public void UpdateUpgradePrice(int newPrice)
    {
        upgradePrice = newPrice;
        SavePlayerStats();
    }

    public void UpdateUpgradeCount(int newCount)
    {
        upgradeCount = newCount;
        SavePlayerStats();
    }

    public void OnApplicationQuit() 
    {
        // Excluir cada chave individualmente
        PlayerPrefs.DeleteKey("BulletInterval");
        PlayerPrefs.DeleteKey("BulletsPerShot");
        PlayerPrefs.DeleteKey("BulletSpeed");
        PlayerPrefs.DeleteKey("Damage");
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.DeleteKey("PlayerLevel");
        PlayerPrefs.DeleteKey("UpgradeCount");
        PlayerPrefs.DeleteKey("Level1Initialized");

        // Salvar as alterações
        PlayerPrefs.Save();
    }
}
