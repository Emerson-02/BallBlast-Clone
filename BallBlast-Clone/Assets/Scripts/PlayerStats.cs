using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float bulletInterval = 0.5f; // Intervalo entre as balas em segundos
    public int bulletsPerShot = 1; // Número de balas disparadas de uma vez
    public float bulletSpeed = 10f; // Velocidade das balas
    public int damage = 1; // Dano causado pelas balas
    public int coins = 0; // Moedas coletadas
}
