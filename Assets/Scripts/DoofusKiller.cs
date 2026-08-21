using UnityEngine;
using System.Collections;

public class DoofusKiller : MonoBehaviour
{
    [SerializeField] private DoofusController con;
    private Animator anim;
    private bool hasTriggeredDeath;

    void OnTriggerStay(Collider other)
    {
        if (hasTriggeredDeath) return;
        bool destroyedPlat = false;
        if (other.gameObject.GetComponent<killPulpit>() != null)
        {
            destroyedPlat = other.gameObject.GetComponent<killPulpit>().isDestroyed;
        }

        bool intoVoid = other.gameObject.name == "KillerVoid";

        if (destroyedPlat || intoVoid)
        {
            GetComponent<Collider>().enabled = false;
            hasTriggeredDeath = true;
            con.enabled = false;
            anim.SetTrigger("die");
            StartCoroutine(DestroyPulpitsAfterDelay(0.5f));
            ScoreManager scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
            scoreManager.GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        bool destroyedPlat = false;
        if (other.gameObject.GetComponent<killPulpit>() != null)
        {
            destroyedPlat = other.gameObject.GetComponent<killPulpit>().isDestroyed;
        }

        if (!destroyedPlat)
        {
            ScoreManager scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
            scoreManager.score += 1;
            scoreManager.UpdateScore();
        }
    }

    private IEnumerator DestroyPulpitsAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        GameObject[] pulpits = GameObject.FindGameObjectsWithTag("Pulpit");
        foreach (GameObject pulpit in pulpits)
        {
            Destroy(pulpit);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        con = gameObject.GetComponent<DoofusController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
