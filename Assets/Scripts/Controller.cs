using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;    // For using TextMeshPro-Text component of Score GameObject
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public GameObject[] Cards;
    public Sprite[] Images;

    // Card IDs for pairs of cards
    private int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };

    // Used for tracking revealed cards
    private MemoryGame firstRevealedCard;
    private MemoryGame secondRevealedCard;
    private int revealedCardCounter = 0;

    // Track score
    private int score;

    // Reference to GUI object
    public GameObject scoreLabel;


    void Start()
    {
        // Shuffle card IDs
        int[] shuffled = Shuffle(numbers);

        // References
        SpriteRenderer face;
        MemoryGame obj;
        int id;

        // Assign card face to objects based on card ID
        for (int i = 0; i < Cards.Length; i++)
        {
            face = Cards[i].GetComponent<SpriteRenderer>();

            // Face based on ID
            id = shuffled[i];
            face.sprite = Images[id];

            // Let the object know it's card ID
            obj = Cards[i].GetComponent<MemoryGame>();
            obj.ID = id;

        }
    }

    // Fisher-Yates shuffle algorithm, called to shuffle card IDs
    private int[] Shuffle(int[] nums)
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    // Algorithm to compare two revealed cards for match
    public void CompareRevealedCard(MemoryGame revealedCard)
    {
        // Get references to two revealed cards
        if (this.revealedCardCounter == 0)
        {
            this.firstRevealedCard = revealedCard;
            this.revealedCardCounter += 1;
        }
        else
        {
            this.secondRevealedCard = revealedCard;
            this.revealedCardCounter += 1;
        }

        // Compare the two revealed cards for match
        if (this.revealedCardCounter == 2)
        {
            // If the two cards match...
            if (firstRevealedCard.ID == secondRevealedCard.ID)
            {
                // Score
                this.score += 1;
                this.scoreLabel.GetComponent<TextMeshPro>().text = "SCORE  " + this.score;

                // Dim the colors
                firstRevealedCard.Animate();
                secondRevealedCard.Animate();

                // Reset references
                firstRevealedCard = null;
                secondRevealedCard = null;

                // Reset counter
                this.revealedCardCounter = 0;
            }
            else
            {
                // Punish
                if (this.score > 0)
                {
                    this.score -= 1;
                    this.scoreLabel.GetComponent<TextMeshPro>().text = "SCORE  " + this.score;
                }

                // After a second, unreveal the first card
                StartCoroutine(Wait(firstRevealedCard));

                // First card is now the second card
                firstRevealedCard = secondRevealedCard;

                // Reset second card for reuse
                secondRevealedCard = null;
                this.revealedCardCounter = 1;
            }
        }
    }

    // Called to unreveal a card after x seconds
    public IEnumerator Wait(MemoryGame card)
    {
        yield return new WaitForSeconds(0.09f);

        card.Unreveal();
    }
    
    // Called to reload the scene (restart)
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
