using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CannonScript : MonoBehaviour
{
    public Transform wheelLeft, WheelRight;
    public float speed = 5f;
    public float wheelRotationSpeed = 360f; // Velocidade de rotação das rodas

    private float screenLeftLimit;
    private float screenRightLimit;
    private float halfCannonWidth;

    public bool isDragging = false;
    private Vector3 lastMousePosition;

    public bool canMove = true;

    public JewelSpawner jewelSpawner;
    public PlayerStats playerStats;
    public UIController uiController;

    private void Awake() 
    {
        uiController = FindObjectOfType<UIController>();
        playerStats = FindObjectOfType<PlayerStats>();
        jewelSpawner = FindObjectOfType<JewelSpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Calcular os limites da tela em coordenadas do mundo
        Camera mainCamera = Camera.main;
        screenLeftLimit = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z)).x;
        screenRightLimit = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, transform.position.z)).x;

        // Calcular a metade da largura do canhão
        halfCannonWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 movement = currentMousePosition - lastMousePosition;
                movement.z = 0; // Garantir que o movimento seja apenas no plano XY
                movement.y = 0; // Ignorar o movimento no eixo Y

                // Limitar a posição do canhão dentro dos limites da tela ajustados
                Vector3 newPosition = transform.position + movement;
                newPosition.x = Mathf.Clamp(newPosition.x, screenLeftLimit + halfCannonWidth, screenRightLimit - halfCannonWidth);
                movement = newPosition - transform.position; // Atualizar o movimento com base na posição limitada
                transform.position = newPosition;

                // Calcular a rotação das rodas
                float wheelRotation = movement.x * (wheelRotationSpeed/2);

                // Aplicar a rotação às rodas
                wheelLeft.Rotate(Vector3.forward, -wheelRotation);
                WheelRight.Rotate(Vector3.forward, -wheelRotation);

                lastMousePosition = currentMousePosition;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Jewel"))
        {
            jewelSpawner.StopSpawning();

            canMove = false;
            isDragging = false;

            // pega todos os objetos com a tag Jewel na cena
            GameObject[] jewels = GameObject.FindGameObjectsWithTag("Jewel");

            // para cada objeto Jewel na cena
            foreach (GameObject jewel in jewels)
            {
                // pega o componente Rigidbody2D do objeto Jewel
                Rigidbody2D rb = jewel.GetComponent<Rigidbody2D>();

                // muda para kinematic
                rb.bodyType = RigidbodyType2D.Kinematic;

                // para o objeto Jewel
                rb.velocity = Vector2.zero;

                // para a rotação do objeto Jewel
                rb.angularVelocity = 0;

            }



            // FlashRed
            StartCoroutine(other.gameObject.GetComponent<Jewel>().FlashRed());
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Coin"))
        {
            playerStats.coins++;
            uiController.UpdateCoins(playerStats.coins);
            Destroy(other.gameObject);
            
        }
       
    }
}
