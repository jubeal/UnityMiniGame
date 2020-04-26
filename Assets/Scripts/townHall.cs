using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class townHall : MonoBehaviour, IHealth
{
    public float CurrentHealth { get; set; }
    private float time = 0;
    private GameObject soldier;
    private GameObject clone;
    public int buildings = 4;
    // Start is called before the first frame update
    void Start()
    {
        if (tag == "Human")
            soldier = GameObject.Find("Human");
        else if (tag == "Orc")
            soldier = GameObject.Find("Orc");
        clone = Instantiate(soldier, transform.position + new Vector3(1, -1, -1), Quaternion.identity);
        clone.GetComponent<SpriteRenderer>().enabled = true;
        if (tag == "Orc")
            clone.GetComponent<AIEnnemi>().enabled = true;
        CurrentHealth = 400;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (CurrentHealth <= 0)
        {
            if (tag == "Orc")
                SceneManager.LoadScene("Victory");
            else
                SceneManager.LoadScene("Defeat");
        }

        if (time >= 20.0f - 2.5f * buildings)
        {
            clone = Instantiate(soldier, transform.position + new Vector3(1, -1, -1), Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().enabled = true;
            if (tag == "Orc")
                clone.GetComponent<AIEnnemi>().enabled = true;
            time = 0;
        }
    }
}
