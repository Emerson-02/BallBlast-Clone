using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jewel : MonoBehaviour
{
    public int jewelValue;
    public TextMeshPro valueText;
    public PlayerStats playerStats;


    private void Awake() 
    {
        playerStats = FindObjectOfType<PlayerStats>();

        valueText = GetComponentInChildren<TextMeshPro>();
        valueText.text = jewelValue.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Bullet"))
        {
            //other.GetComponent<PlayerStats>().jewels += jewelValue;

            TakeDamage(playerStats.damage);

            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        jewelValue -= damage;
        valueText.text = jewelValue.ToString();

        if (jewelValue <= 0)
        {
            // animação de destruição

            // fazer moedas caírem

            Destroy(gameObject);
        }
    }
}
