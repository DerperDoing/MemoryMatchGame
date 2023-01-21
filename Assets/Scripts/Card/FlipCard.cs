using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipCard : MonoBehaviour
{
    [SerializeField]
    private float flipSpeed = 10f;

    [Space]
    [SerializeField]
    private GameObject cardFrontGO;

    [SerializeField]
    private GameObject cardBackGO;    

    private Button button; 
    private bool facedUp;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    private void Start()
    {
        coroutine = null;
        facedUp = false;

        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnMouseDown);
    }

    private void OnMouseDown()
    {
        if (coroutine == null)
        {
            coroutine = Flip();
            StartCoroutine(coroutine);
        }
    }  

    IEnumerator Flip()
    {        
        Quaternion startRotation = transform.rotation;

        int finalYRot = facedUp ? 0 : 180;        
        Quaternion endRotation = Quaternion.Euler(0, finalYRot, 0);

        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime * flipSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, timer);
            if (timer >= 0.5f)
            {
                cardBackGO.SetActive(facedUp);
                cardFrontGO.SetActive(!facedUp);                
            }
            yield return null;
        }
        transform.rotation = endRotation;

        facedUp = !facedUp;

        StopCoroutine(coroutine);
        coroutine = null;
    }
}
