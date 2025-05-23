using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageIndicator : MonoBehaviour
{
    public Image image;

    public float flashSpeed;
    
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    public void Flash()
    {
        Debug.Log("화면 깜빡임");
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        image.enabled = true;
        image.color = new Color(1f, 100f/ 255f, 100f/ 255f,1f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 1f;
        float a = startAlpha;

        while (a > 0)
        {
            
            a -=(startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f/ 255f, 100f/ 255f, a);
            yield return null;  
        } 
        image.enabled = false;
    }  
}
