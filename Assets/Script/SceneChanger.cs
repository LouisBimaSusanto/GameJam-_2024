using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Animator Anim;
    public Image Img;

    public void L_Fitur()
    {
        //SceneManager.LoadScene ("L_Fitur");
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Img.color.a == 1);
        SceneManager.LoadScene("L_Fitur");
    }
}
