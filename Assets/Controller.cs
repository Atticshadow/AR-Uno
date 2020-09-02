using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public SpriteRenderer ARCard;

    public Button[] playerHand;
    private int playerHandSize = 7;
    public Button draw;
    bool playedACard = false;

    System.Random rng = new System.Random();

    public Sprite[] cardSprites;
    private Sprite[] AIHand = new Sprite[7];
    private int handSize = 7;
    public Text AIHandSize;
    public Text Info;
    private bool busy = false;
    private bool gameOver = false;

    // Use this for initialization
    void Start () {
        ARCard.sprite = GetRandomCard();
        AIHand[0] = GetRandomCard();
        AIHand[1] = GetRandomCard();
        AIHand[2] = GetRandomCard();
        AIHand[3] = GetRandomCard();
        AIHand[4] = GetRandomCard();
        AIHand[5] = GetRandomCard();
        AIHand[6] = GetRandomCard();
        playerHand[0].image.sprite = GetRandomCard();
        playerHand[1].image.sprite = GetRandomCard();
        playerHand[2].image.sprite = GetRandomCard();
        playerHand[3].image.sprite = GetRandomCard();
        playerHand[4].image.sprite = GetRandomCard();
        playerHand[5].image.sprite = GetRandomCard();
        playerHand[6].image.sprite = GetRandomCard();
    }
	
	// Update is called once per frame
	void Update () {
        if (handSize == 0 || playerHandSize == 0)
        {
            if(handSize == 0){
                Info.text = "I win!";
                gameOver = true;
            }
            else if (playerHandSize == 0)
            {
                Info.text = "You win!";
                gameOver = true;
            }
        }
        if ((playedACard == true && busy == false) && gameOver == false)
        {
            Info.text = "My Turn";
            StartCoroutine(AITurn());
        }
        else if ((playedACard == false) && gameOver == false)
        {
            Info.text = "Your Turn";
        }
    }

    private IEnumerator AITurn()
    {
        busy = true;
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 7; i++)
        {
            //Info.text = "Current card: " + AIHand[i].name.Substring(AIHand[i].name.Length-1) + " " +i;
            //yield return new WaitForSeconds(1.0f);
            if (AIHand[i].name.Equals("Wild") && playedACard == true)
            {
                ARCard.sprite = AIHand[i];
                if (!(AIHand[i].name.Equals("BackOfCard")))
                {
                    AIHand[i] = cardSprites[54];
                }
                yield return new WaitForSeconds(1.0f);
                playedACard = true;
                --handSize;
                AIHandSize.text = handSize + "";
            }
            else if (AIHand[i].name.Equals("WildD4") && playedACard == true)
            {
                ARCard.sprite = AIHand[i];
                DrawClick();
                DrawClick();
                DrawClick();
                DrawClick();
                if (!(AIHand[i].name.Equals("BackOfCard")))
                {
                    AIHand[i] = cardSprites[54];
                }
                playedACard = true;
                --handSize;
                AIHandSize.text = handSize + "";
            }
            else if ((AIHand[i].name.Contains(ARCard.sprite.name.Substring(0, ARCard.sprite.name.Length - 1)) ||
                      AIHand[i].name.Contains(ARCard.sprite.name.Substring(ARCard.sprite.name.Length - 1)) || ARCard.sprite.name.Contains("Wild")) && playedACard == true)
            {
                ARCard.sprite = AIHand[i];
                if (AIHand[i].name.Contains("S"))
                {
                    playedACard = true;
                }
                else if (AIHand[i].name.Contains("D"))
                {
                    DrawClick();
                    DrawClick();
                    playedACard = true;
                }
                else
                {
                    playedACard = false;
                }
                if (!(AIHand[i].name.Equals("BackOfCard")))
                {
                    AIHand[i] = cardSprites[54];
                }
                --handSize;
                AIHandSize.text = handSize + "";
            }
            else if (i == 6 && playedACard == true)
            {
                AIDrawCard();
            }
        }
        busy = false;
    }

    public void AIDrawCard(){
        bool cardReplaced = false;
        for (int i = 0; i < 7; i++)
        {
            if (AIHand[i].name.Equals("BackOfCard"))
            {
                ++handSize;
                AIHandSize.text = handSize + "";
                AIHand[i] = GetRandomCard();
                cardReplaced = true;
            }
        }
        if (cardReplaced == false)
        {
            int rand = rng.Next(0, 6);
            AIHand[rand] = GetRandomCard();
        }
    }

    public void CardClick(int cardNum){
        bool replace = false;
        if (cardNum == 1)
        {
            replace = CheckCard(playerHand[0].image.sprite);
            if (!(playerHand[0].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[0].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 2)
        {
            replace = CheckCard(playerHand[1].image.sprite);
            if (!(playerHand[1].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[1].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 3)
        {
            replace = CheckCard(playerHand[2].image.sprite);
            if (!(playerHand[2].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[2].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 4)
        {
            replace = CheckCard(playerHand[3].image.sprite);
            if (!(playerHand[3].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[3].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 5)
        {
            replace = CheckCard(playerHand[4].image.sprite);
            if (!(playerHand[4].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[4].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 6)
        {
            replace = CheckCard(playerHand[5].image.sprite);
            if (!(playerHand[5].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[5].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
        else if (cardNum == 7)
        {
            replace = CheckCard(playerHand[6].image.sprite);
            if (!(playerHand[6].image.sprite.name.Equals("BackOfCard")) && replace == true)
            {
                playerHand[6].image.sprite = cardSprites[54];
                --playerHandSize;
            }
        }
    }

    public Sprite GetRandomCard(){
        int random = rng.Next(0, 54);
        return cardSprites[random];
    }

    public void DrawClick()
    {
        int cardsInHand = 7;
        for (int i = 0; i < 7; i++)
        {
            if (playerHand[i].image.sprite.name.Equals("BackOfCard"))
            {
                cardsInHand = cardsInHand - 1;
            }
        }
        if (cardsInHand == 7)
        {
            int random = rng.Next(1, 7);
            playerHand[random].image.sprite = GetRandomCard();
        }
        else
        {
            bool cardGiven = false;
            for (int i = 0; i < 7; i++)
            {
                if (playerHand[i].image.sprite.name.Equals("BackOfCard") && cardGiven == false)
                {
                    playerHand[i].image.sprite = GetRandomCard();
                    cardGiven = true;
                    ++playerHandSize;
                }
            }
        }
    }

    public bool CheckCard(Sprite sprite){
        if (sprite.name.Equals("Wild") && playedACard == false)
        {
            ARCard.sprite = sprite;
            playedACard = false;
            return true;
        }
        else if (sprite.name.Equals("WildD4") && playedACard == false)
        {
            ARCard.sprite = sprite;
            AIDrawCard();
            AIDrawCard();
            AIDrawCard();
            AIDrawCard();
            playedACard = false;
            return true;
        }
        else if ((sprite.name.Contains(ARCard.sprite.name.Substring(0, ARCard.sprite.name.Length - 1)) || 
                  sprite.name.Contains(ARCard.sprite.name.Substring(ARCard.sprite.name.Length - 1)) || ARCard.sprite.name.Contains("Wild")) && playedACard == false)
        {
            ARCard.sprite = sprite;
            if (sprite.name.Contains("S"))
            {
                playedACard = false;
            }
            else if (sprite.name.Contains("D"))
            {
                AIDrawCard();
                AIDrawCard();
                playedACard = false;
            }
            else
            {
                playedACard = true;
            }
            return true;
        }
        return false;
    }
}
