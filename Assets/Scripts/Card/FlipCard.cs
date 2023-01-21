using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCard : MonoBehaviour
{
    [SerializeField]
    private float flipSpeed = 1.5f;

    [Space]
    [SerializeField]
    private GameObject cardFrontGO;

    [SerializeField]
    private GameObject cardBackGO;    
    
    private IEnumerator coroutine;

    public void Flip(bool isFaceUp, Action flipped = null)
    {        
        if (coroutine == null)
        {
            coroutine = FlipCoRo(isFaceUp, flipped);
            StartCoroutine(coroutine);
        }
    }  

    IEnumerator FlipCoRo(bool isFaceUp, Action flipped)
    {        
        Quaternion startRotation = transform.rotation;

        int finalYRot = isFaceUp ? 0 : 180;        
        Quaternion endRotation = Quaternion.Euler(0, finalYRot, 0);

        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime * flipSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, timer);
            if (timer >= 0.5f)
            {
                cardBackGO.SetActive(isFaceUp);
                cardFrontGO.SetActive(!isFaceUp);                
            }
            yield return null;
        }
        transform.rotation = endRotation;        

        StopCoroutine(coroutine);
        coroutine = null;

        flipped?.Invoke();
    }
}
