using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Memory.Constant;

namespace Memory.UI
{
    public class ModalWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI headerText;
        [SerializeField] Image image;
        [SerializeField] TextMeshProUGUI contentText;
        [SerializeField] Sprite victoryImage;
        [SerializeField] Sprite failureImage;

        public void ShowResult(int points)
        {
            if(points >= 0)
            {
                headerText.text = ConstantValues.victoryText;
                image.sprite = victoryImage;
                contentText.text = $"You have collected {points} points";
            }
            else
            {
                headerText.text = ConstantValues.failureText;
                image.sprite = failureImage;
                contentText.text = $"You have collected {points} points";
            }
            
        }
    }
}