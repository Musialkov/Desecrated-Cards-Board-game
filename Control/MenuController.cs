using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.Control
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] Canvas gameCanvas;
        [SerializeField] Canvas menuCanvas;

        GameController gameController;
        
        private void Awake() 
        {
            gameController = GetComponent<GameController>();
        }
        void Start()
        {
            ShowGameOptions();
        }

        public void ShowGameOptions()
        {
            gameCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
        }

        public void ChooseGameOption(int option)
        {
            menuCanvas.gameObject.SetActive(false);
            gameCanvas.gameObject.SetActive(true);
            gameController.StartGame(option);
        }
    }
}
