using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCard : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed = 2f;

    [SerializeField]
    private Vector2 targetScale = Vector2.zero;

    private IEnumerator coroutine;

    public void Zoom(Action zoomed = null)
    {
        if (coroutine == null)
        {
            coroutine = ZoomCoRo(targetScale, zoomed);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator ZoomCoRo(Vector2 targetScale, Action zoomed)
    {
        Vector3 startScale = transform.localScale;                

        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime * zoomSpeed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer);            
            yield return null;
        }
        transform.localScale = targetScale;

        StopCoroutine(coroutine);
        coroutine = null;

        zoomed?.Invoke();
    }
}
