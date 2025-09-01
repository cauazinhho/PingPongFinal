using System.Collections;
using UnityEngine;

public class MustacheAnim : MonoBehaviour
{
    float size = 1000f;
    
    float sizeScale = 1f;

    int ativado = 0;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.localScale = new Vector3(sizeScale, 1, 1);
    }

    public void startCorrotina()
    {
        StartCoroutine(MustAnim());
    }

    public void startAnim()
    {
        animator.SetTrigger("OpenMouth");
    }

    public IEnumerator MustAnim()
    {
        float duration = 0.1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            size = Mathf.Lerp(1, 1.4f, elapsed / duration);
            sizeScale = size / 1;
            yield return null;
        }
        
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(MustAnim2());
    }
    public IEnumerator MustAnim2()
    {
        ativado++;
        float duration = 0.1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            size = Mathf.Lerp(14, 10, elapsed / duration);
            sizeScale = size / 10;
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        if (ativado < 4)
        {
            StartCoroutine(MustAnim());
        }
        else
        {
            ativado = 0;
        }
    }
}
