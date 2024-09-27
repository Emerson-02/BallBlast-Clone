using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<int> levelJewels;
    public int levelJewelsCount;
    public JewelSpawner jewelSpawner;
    public UIController uiController;
    public CannonScript cannonScript;

    private void Awake() {
        cannonScript = FindObjectOfType<CannonScript>();
        jewelSpawner = FindObjectOfType<JewelSpawner>();
        uiController = FindObjectOfType<UIController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelJewelsCount == 0)
        {
            cannonScript.canMove = false;
            cannonScript.isDragging = false;

            uiController.panelWin.SetActive(true);
        }
    }
}
