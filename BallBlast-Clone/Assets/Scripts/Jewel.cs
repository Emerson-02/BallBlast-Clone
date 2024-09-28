using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jewel : MonoBehaviour
{
    public int jewelValue;
    public int originalValue;
    public TextMeshPro valueText;
    public PlayerStats playerStats;

    public float horizontalForce = 5f; // Força horizontal aplicada ao objeto

    private Rigidbody2D rb;
    public JewelSpawner jewelSpawner;
    public float minBounceVelocity = 0.1f; // Velocidade mínima de quique para adicionar força

    public GameController gameController;


    private void Awake() 
    {
        playerStats = FindObjectOfType<PlayerStats>();
        jewelSpawner = FindObjectOfType<JewelSpawner>();
        gameController = FindObjectOfType<GameController>();    

        valueText = GetComponentInChildren<TextMeshPro>();
        valueText.text = jewelValue.ToString();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
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
            // Verifica a escala da joia na lista de escalas
            int index = jewelSpawner.scales.IndexOf(transform.localScale);

            GameObject coinPrefab = Resources.Load<GameObject>("Coin");

            if(index > 0)
            {
                SpawnMinorJewels(index, originalValue / 2, transform.position, 1);
                SpawnMinorJewels(index, originalValue / 2, transform.position, -1);

                // Instancia index moedas a partir da posição da joia
                for (int i = 0; i < index; i++)
                {
                    GameObject newCoin = Instantiate(coinPrefab, gameObject.transform.position, Quaternion.identity);
                    newCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)) * 5f, ForceMode2D.Impulse);
                }

                gameController.levelJewelsCount--;

                Destroy(gameObject);
            }
            else
            {
                // Instancia uma moeda a partir da posição da joia
                GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                gameController.levelJewelsCount--;

                Destroy(gameObject);
            }
        }
    }

    public IEnumerator FlashRed()
    {
        for (int i = 0; i < 10; i++)
        {
            valueText.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            valueText.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void SpawnMinorJewels(int index, int value, Vector3 position, int direction)
    {
        // instanciar a joia no ponto onde está a joia original
        GameObject newJewel = Instantiate(jewelSpawner.jewelPrefab, position, Quaternion.identity);

        // muda o valor da joia e o originalValue
        newJewel.GetComponent<Jewel>().jewelValue = value;
        newJewel.GetComponent<Jewel>().originalValue = value;

        // muda o texto da joia
        newJewel.GetComponentInChildren<TextMeshPro>().text = value.ToString();

        // pega a coin de Resources
        GameObject coinPrefab = Resources.Load<GameObject>("Coin");


        if(index == 3)
        {
            // atribuir o sprite verde
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSpawner.jewelSprites[2];

            // Atribuir a escala da joia
            newJewel.transform.localScale = jewelSpawner.scales[2];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 1;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 1;
        }
        else if(index == 2)
        {
            // atribuir o sprite azul
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSpawner.jewelSprites[1];

            // Atribuir a escala da joia
            newJewel.transform.localScale = jewelSpawner.scales[1];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 2;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 2;
        }
        else if(index == 1)
        {
            // atribuir o sprite roxo
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSpawner.jewelSprites[0];

            // Atribuir a escala da joia
            newJewel.transform.localScale = jewelSpawner.scales[0];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 3;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 3;
        }

        if(direction == 1)
        {
            newJewel.GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontalForce, ForceMode2D.Impulse);
        }
        else
        {
            newJewel.GetComponent<Rigidbody2D>().AddForce(Vector2.left * horizontalForce, ForceMode2D.Impulse);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a velocidade vertical é menor que a velocidade mínima de quique
        if (Mathf.Abs(rb.velocity.y) < minBounceVelocity)
        {
            // Adicionar uma força na direção horizontal em que a joia está se movendo
            Vector2 horizontalForceDirection = new Vector2(rb.velocity.x, 0).normalized;
            rb.AddForce(horizontalForceDirection * horizontalForce, ForceMode2D.Impulse);
        }
    }

}
