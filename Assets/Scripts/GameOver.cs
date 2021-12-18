using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject splat;
    private AnimUI anim;
    [SerializeField] private GameObject toasted;
    [SerializeField] private GameObject button;

    [SerializeField] private Image render;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = splat.GetComponent<AnimUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.enabled == false)
        {
            button.SetActive(true);
            toasted.SetActive(true);
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        Color c = render.color;
        for (float alpha = 0f; alpha < 1f; alpha += 0.1f)
        {
            c.a = alpha;
            render.color = c;
        }
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            render.color = c;
            yield return null;
        }

    }
    public void getTitle()
    {
        SceneManager.LoadScene(0);
    }
}
