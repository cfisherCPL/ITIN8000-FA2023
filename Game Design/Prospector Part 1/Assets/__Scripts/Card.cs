using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Dynamic")]
    public char suit; // Suit of the Card (C,D,H, or S)
    public int rank; // Rank of the Card (1-13)
    public Color color = Color.black; // Color to tint pips
    public string colS = "Black"; // or "Red". Name of the Color
    public GameObject back; // The GameObject of the back of the card
    public JsonCard def; // The card layout as defined in JSON_Deck.json
 
    // This List holds all of the Decorator GameObjects
    public List<GameObject> decoGOs = new List<GameObject>();
    // This List holds all of the Pip GameObjects
    public List<GameObject> pipGOs = new List<GameObject>();
 
    /// <summary>
    /// Creates this Card’s visuals based on suit and rank.
    /// Note that this method assumes it will be passed a valid suit and rank.
    /// </summary>
    /// <param name="eSuit">The suit of the card (e.g., ’C’)</param>
    /// <param name="eRank">The rank from 1 to 13</param>
    /// <returns></returns>
    public void Init(char eSuit, int eRank, bool startFaceUp = true)
    {
        // Assign basic values to the Card
        gameObject.name = name = eSuit.ToString() + eRank;
        suit = eSuit;
        rank = eRank;
        // If this is a Diamond or Heart, change the default Black color to Red
        if (suit == 'D' || suit == 'H')
            {
            colS = "Red";
            color = Color.red;
            }

        def = JsonParseDeck.GET_CARD_DEF(rank);

        // Build the card from Sprites                                        // a
        AddDecorators();

    }

    /// <summary>
    /// Shortcut for setting transform.localPosition.
    /// </summary>
    /// <param name="v"></param>
    public virtual void SetLocalPos(Vector3 v)
    {                              // b
        transform.localPosition = v;
    }

    // These private variables that will be reused several times                    // c
    private Sprite _tSprite = null;
    private GameObject _tGO = null;
    private SpriteRenderer _tSRend = null;
    // An Euler rotation of 180° around the Z-axis will flip sprites upside down
    private Quaternion _flipRot = Quaternion.Euler(0, 0, 180);

    /// <summary>
    /// Adds the decorators to the top-left and bottom-right of each card.
    ///  Decorators are the suit and rank in the corners of each card.
    /// </summary>
    private void AddDecorators()
    {
        // Add Decorators
        foreach (JsonPip pip in JsonParseDeck.DECORATORS)
        {                         // e
            if (pip.type == "suit")
            {
                // Instantiate a Sprite GameObject
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);       // f
                // Get the SpriteRenderer Component
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                // Get the suit Sprite from the CardSpritesSO.SUIT static field
                _tSRend.sprite = CardSpritesSO.SUITS[suit];
            }
            else
            {
                _tGO = Instantiate<GameObject>(Deck.SPRITE_PREFAB, transform);       // f
                _tSRend = _tGO.GetComponent<SpriteRenderer>();
                // Get the rank Sprite from the CardSpritesSO.RANK static field
                _tSRend.sprite = CardSpritesSO.RANKS[rank];
                // Set the color of the rank to match the suit
                _tSRend.color = color;
            }


            // Make the Decorator Sprites render above the Card
            _tSRend.sortingOrder = 1;                                               // g
                                                                                    // Set the localPosition based on the location from DeckXML
            _tGO.transform.localPosition = pip.loc;
            // Flip the decorator if needed
            if (pip.flip) _tGO.transform.rotation = _flipRot;                       // h
                                                                                    // Set the scale to keep decorators from being too big
            if (pip.scale != 1)
            {
                _tGO.transform.localScale = Vector3.one * pip.scale;
            }
            // Name this GameObject so it’s easy to find in the Hierarchy
            _tGO.name = pip.type;
            // Add this decorator GameObject to the List card.decoGOs
            decoGOs.Add(_tGO);
        }
    }
}
