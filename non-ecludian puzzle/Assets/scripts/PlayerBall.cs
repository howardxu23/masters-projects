using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBall : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 2;
    [SerializeField]
    private float timer;
    [SerializeField]
    GameObject endscreen;
    [SerializeField]
    Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        endscreen.SetActive(false);
        resetButton.onClick.AddListener(resetlevel);
    }


    void resetlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void increaseSize()
    {
        StartCoroutine(increaseSizecorutine(gameObject));
    }
    public void decreaseSize()
    {
        StartCoroutine(decreaseSizecorutine(gameObject));
    }
         IEnumerator increaseSizecorutine(GameObject player)
    {
        if (timer > 0)
        {
            yield break;
        }

        var playerscale = player.transform.localScale;
        while (timer < cooldown)
        {
            player.transform.localScale = Vector3.Lerp(playerscale, playerscale * 2, timer / cooldown);
            timer += Time.deltaTime;
            yield return null;

        }
        player.transform.localScale = playerscale * 2;
        timer = 0;
    }
     IEnumerator decreaseSizecorutine(GameObject player)
    {
        if (timer > 0)
        {
            yield break;
        }
        var playerscale = player.transform.localScale;
        while (timer < cooldown)
        {
            player.transform.localScale = Vector3.Lerp(playerscale, playerscale / 2, timer / cooldown);
            timer += Time.deltaTime;
            yield return null;

        }
        player.transform.localScale = playerscale / 2;
        timer = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            
            endscreen.SetActive(true);
        }

    }
}
