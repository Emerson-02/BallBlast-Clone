using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JewelSpawner : MonoBehaviour
{
    public Sprite[] jewelSprites; // Lista de sprites de joias
    public GameObject jewelPrefab; // Prefab da joia
    public Transform[] spawnPoints; // Pontos de spawn das joias

    public Coroutine currentSpawnRoutine; // Referência para a rotina de spawn atual

    // Lista de escalas
    public List<Vector3> scales = new List<Vector3>
    {
        // Verde - Order in Layer 3 
        new Vector3(0.0586523339f,0.0586523339f,0.0586523339f),
        // Azul - Order in Layer 2
        new Vector3(0.119304717f,0.119304717f,0.119304717f),
        // Roxo - Order in Layer 1
        new Vector3(0.194180354f,0.194180354f,0.194180354f),
        // Vermelho - Order in Layer 0
        new Vector3(0.331485301f,0.331485301f,0.331485301f)
    };

    public GameController gameController;

    private void Awake() {
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelScript();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnJewels(int value)
    {
        // escolher um ponto de spawn aleatório
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instanciar a joia no ponto de spawn
        GameObject newJewel = Instantiate(jewelPrefab, spawnPoint.position, Quaternion.identity);

        // Desabilitar o rigidbody da joia
        newJewel.GetComponent<Rigidbody2D>().simulated = false;

        // Desabilitar o collider da joia
        newJewel.GetComponent<Collider2D>().enabled = false;

        // Atribuir um valor à joia
        newJewel.GetComponent<Jewel>().jewelValue = value;
        newJewel.GetComponent<Jewel>().originalValue = value;

        // mudar o Texto da joia
        newJewel.GetComponentInChildren<TextMeshPro>().text = value.ToString();

        // Atribuir um sprite à joia
        if(value < 8)
        {
            // atribuir o sprite verde
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSprites[0];

            // Atribuir a escala da joia
            newJewel.transform.localScale = scales[0];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 3;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 3;
        }
        else if(value < 16)
        {
            // atribuir o sprite azul
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSprites[1];

            // Atribuir a escala da joia
            newJewel.transform.localScale = scales[1];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 2;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 2;
        }
        else if(value < 32)
        {
            // atribuir o sprite roxo
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSprites[2];

            // Atribuir a escala da joia
            newJewel.transform.localScale = scales[2];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 1;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 1;
        }
        else
        {
            // atribuir o sprite vermelho
            newJewel.GetComponent<SpriteRenderer>().sprite = jewelSprites[3];

            // Atribuir a escala da joia
            newJewel.transform.localScale = scales[3];

            // alterar o order in layer
            newJewel.GetComponent<SpriteRenderer>().sortingOrder = 0;

            // alterar o order in layer do texto
            newJewel.GetComponentInChildren<TextMeshPro>().sortingOrder = 0;
        }

        // move a joia até estar completamente dentro da área de jogo
        StartCoroutine(MoveJewelRoutine(newJewel, spawnPoint));

        
    }

    public void LevelScript()
    {
        // Pega o nome da Cena
        string sceneName = SceneManager.GetActiveScene().name;

        // pega o que tiver depois do "-"
        string level = sceneName.Split('-')[1];

        // Converte o level para int
        int levelInt = int.Parse(level);

        // switch
        switch (levelInt)
        {
            case 1:
                // Chama o Roteiro para a fase específica
                StartSpawning(new List<int> { 4, 4, 8});
                break;
            case 2:
                // Chama o Roteiro para a fase específica
                StartSpawning(new List<int> { 4, 4, 8, 8, 16});
                break;
            case 3:
                // Chama o Roteiro para a fase específica
                StartSpawning(new List<int> { 4, 4, 8, 8, 16, 16, 32});
                break;
            case 4:
                // Chama o Roteiro para a fase específica
                StartSpawning(new List<int> { 4, 4, 8, 8, 16, 16, 32, 32, 64});
                break;
            case 5:
                // Chama o Roteiro para a fase específica
                StartSpawning(new List<int> { 4, 4, 8, 8, 16, 16, 32, 32, 64, 64, 128});
                break;
            default:
                break;
        }

    } 

    public void StartSpawning(List<int> jewelValues)
    {
        if (currentSpawnRoutine == null)
        {
            currentSpawnRoutine = StartCoroutine(SpawnJewelsRoutine(jewelValues));

            // para cada valor na lista de joias
            for (int i = 0; i < jewelValues.Count; i++)
            {
                if(jewelValues[i] < 8)
                {
                    // adiciona à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i]);
                }
                else if(jewelValues[i] < 16)
                {
                    // adiciona à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i]);

                    // adiciona 2x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 2);
                    gameController.levelJewels.Add(jewelValues[i] / 2);

                }
                else if(jewelValues[i] < 32)
                {
                    // adiciona à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i]);

                    // adiciona 2x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 2);
                    gameController.levelJewels.Add(jewelValues[i] / 2);

                    // adiciona 4x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);

                }
                else
                {
                    // adiciona à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i]);

                    // adiciona 2x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 2);
                    gameController.levelJewels.Add(jewelValues[i] / 2);

                    // adiciona 4x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);
                    gameController.levelJewels.Add(jewelValues[i] / 4);

                    // adiciona 8x metade do valor à lista de joias do nivel
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8);
                    gameController.levelJewels.Add(jewelValues[i] / 8); 

                }

                gameController.levelJewelsCount = gameController.levelJewels.Count;
            }
        }
    }

    public void StopSpawning()
    {
        if (currentSpawnRoutine != null)
        {
            StopCoroutine(currentSpawnRoutine);
            currentSpawnRoutine = null;
        }
    }

    public IEnumerator SpawnJewelsRoutine(List<int> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            SpawnJewels(values[i]);
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator MoveJewelRoutine(GameObject jewel, Transform spawnPoint)
    {
        // Obter os limites da tela em coordenadas do mundo
        Camera mainCamera = Camera.main;
        float screenLeftLimit = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, spawnPoint.position.z)).x;
        float screenRightLimit = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, spawnPoint.position.z)).x;

        // Pega a largura inteira da joia
        float jewelWidth = jewel.GetComponent<SpriteRenderer>().bounds.extents.x + jewel.GetComponent<SpriteRenderer>().bounds.extents.x / 4;

        float moveSpeed = 0.005f;

        // Armazena a posição inicial da joia
        Vector3 initialPosition = jewel.transform.position;

        // Mover a joia até que ela esteja completamente dentro da área do jogo
        while (jewel.transform.position.x < screenLeftLimit + jewelWidth || jewel.transform.position.x > screenRightLimit - jewelWidth)
        {
            if (jewel.transform.position.x < screenLeftLimit + jewelWidth)
            {
                jewel.transform.position += new Vector3(moveSpeed, 0, 0);
            }
            else if (jewel.transform.position.x > screenRightLimit - jewelWidth)
            {
                jewel.transform.position += new Vector3(-moveSpeed, 0, 0);
            }
            yield return null;
        }

        // Armazena a posição final da joia
        Vector3 finalPosition = jewel.transform.position;

        // Habilitar o rigidbody da joia
        jewel.GetComponent<Rigidbody2D>().simulated = true;

        // Habilitar o collider da joia
        jewel.GetComponent<Collider2D>().enabled = true;

        // Determinar a direção da força com base na posição inicial e final
        float forceDirection = finalPosition.x > initialPosition.x ? 1f : -1f;

        // Adicionar uma força horizontal ao objeto na direção oposta
        float forceMagnitude = 2f;
        Vector2 force = new Vector2(forceDirection * forceMagnitude, 0);
        jewel.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}
