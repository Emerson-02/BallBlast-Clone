using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
