using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Memory.Models
{
    public class CardButton
    {
            private Button button;
            private int spriteId;

            public Button Button {get; set;}
            public int SpriteId {get; set;}

            public CardButton(Button card, int spriteId)
            {
                this.Button = card;
                this.SpriteId = spriteId;
            }
    }
}
