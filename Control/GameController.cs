using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Memory.Constant;
using Memory.Models;
using Memory.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Memory.Control
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] GameObject cardReference;
        [SerializeField] GameObject cardContainer;
        [SerializeField] GridLayoutGroup layout;
        [SerializeField] List<Sprite> fronts = new List<Sprite>();
        [SerializeField] Sprite back;
        [SerializeField] ScoreController scoreController;
        [SerializeField] ModalWindow modalWindow;

        List<Button> buttons = new List<Button>();
        List<CardButton> cards = new List<CardButton>();
        CardButton currentCard;
        CardButton checkedCard;
        bool playerControl = true;


        public void StartGame(int option)
        {
            ResetPreviousVariables();
            DrawCards(option);
            ShuffleSprites();
            foreach(var card in cards)
            {
                card.Button.onClick.AddListener(() => RevealCard(card));
            }
        }

        private void ResetPreviousVariables()
        {
            currentCard = checkedCard = null;
            layout.enabled = true;
            scoreController.ResetScore();
            modalWindow.gameObject.SetActive(false);
        }

        private void DrawCards(int option)
        {
            DestroyOldCards();
            SetDefaultLayout();

            int x = 2;
            int y = 2;
                
            switch (option)
            {
                case 1:
                    y = 4;
                    break;
                case 2:
                    x = 4;
                    y = 4;
                    layout.constraintCount = 4;
                    layout.cellSize *= ConstantValues.cellScale;
                    layout.spacing = ConstantValues.fourByFourSpacing;
                    break;
            }

            InstantiateNewCards(x*y);            
        }

        private void SetDefaultLayout()
        {
            layout.constraintCount = 2;
            layout.cellSize = ConstantValues.cellSize;
            layout.spacing = ConstantValues.defaultSpacing;
        }

        private void DestroyOldCards()
        {
            buttons.Clear();
            cards.Clear();
            foreach(Transform child in cardContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void InstantiateNewCards(int cardAmount)
        {
            for (int i = 0; i < cardAmount; i++)
            {
                GameObject card = Instantiate(cardReference, cardContainer.transform);
                buttons.Add(card.GetComponent<Button>());
            }   
        }

        private void ShuffleSprites()
        {
            List<int> listOfIndexes = new List<int>();
            listOfIndexes.AddRange(Enumerable.Range(0, buttons.Count));
            List<int> randomized = listOfIndexes.OrderBy(i => Random.Range(0, buttons.Count)).ToList();

            for(int i = 0; i < randomized.Count; i++)
            {
                CardButton card = new CardButton(buttons[randomized[i]], (int) i / 2);
                cards.Add(card);
            }
        }

        private void RevealCard(CardButton card)
        {
            layout.enabled = false;
            if(!playerControl) return;

            if(currentCard == null)
            {
                currentCard = card;
                currentCard.Button.image.sprite = fronts[currentCard.SpriteId];
            }
            else if(checkedCard == null)
            {
                
                if(currentCard.Button.gameObject == card.Button.gameObject) return;
                
                checkedCard = card;
                checkedCard.Button.image.sprite = fronts[checkedCard.SpriteId];
                if(currentCard.SpriteId == checkedCard.SpriteId)
                {
                    scoreController.AddPointsToScore(ConstantValues.pointsForWin);
                    StartCoroutine(RemoveCheckedCards());
                                 
                }
                else
                {
                    scoreController.AddPointsToScore(ConstantValues.pointsForLoose);
                    StartCoroutine(CoverTheCards());               
                } 
            }
        }

        private IEnumerator RemoveCheckedCards()
        {
            playerControl = false;
            yield return new WaitForSeconds(0.5f);
            Destroy(currentCard.Button.gameObject);
            Destroy(checkedCard.Button.gameObject);
            yield return new WaitForFixedUpdate();
            currentCard = checkedCard = null;
            playerControl = true;
            CheckWin();
        }

        private void CheckWin()
        {
            if(layout.transform.childCount <= 0)
            {
                modalWindow.gameObject.SetActive(true);
                modalWindow.ShowResult(scoreController.Score);
            }        
        }

        private IEnumerator CoverTheCards()
        {
            playerControl = false;
            yield return new WaitForSeconds(1);
            playerControl = true;
            currentCard.Button.image.sprite = back;
            checkedCard.Button.image.sprite = back;
            currentCard = checkedCard = null;
        }
    }
}
