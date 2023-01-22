using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    private int cardValue;

    private Collider2D collider;
    private FlipCard cardFlipper;
    private ZoomCard cardScaler;
    private ParticleSystem particleSystem;

    private bool isFaceUp;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {        
        EventAggregator.matchedEvent += OnMatched;
        EventAggregator.twoCardsSelectedEvent += DisableInteraction;
    }

    private void OnDisable()
    {     
        EventAggregator.matchedEvent -= OnMatched;
        EventAggregator.twoCardsSelectedEvent -= DisableInteraction;
    }

    private void OnMouseDown()
    {
        OnCardSelect();        
    }

    private void Start()
    {
        cardFlipper = GetComponent<FlipCard>();
        cardScaler = GetComponent<ZoomCard>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        isFaceUp = true;
        collider.enabled = false;
    }

    public void Init(int cardValue)
    {
        this.cardValue = cardValue;
        GetComponentInChildren<TextMeshPro>(true).text = cardValue.ToString();        
    }

    private void OnMatched(bool matched)
    {
        if (!isFaceUp)
        {
            collider.enabled = true;
        }
        else
        {
            if (!matched)
            {
                cardFlipper.Flip(isFaceUp, () =>
                {
                    isFaceUp = !isFaceUp;
                    collider.enabled = true;
                });            
            }
            else if (matched)
            {
                collider.enabled = false;
                cardScaler.Zoom(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
    }

    private void OnCardSelect()
    {
        particleSystem.Play();

        EventAggregator.cardSelectedEvent?.Invoke();

        cardFlipper.Flip(isFaceUp, () =>
        {
            EventAggregator.flippedCardValEvent?.Invoke(cardValue);
        });

        isFaceUp = !isFaceUp;

        collider.enabled = false;
    }

    private void DisableInteraction()
    {
        collider.enabled = false;
    }
}
