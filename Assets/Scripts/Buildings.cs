using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour, IHealth
{
    public float CurrentHealth { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            if (tag == "Orc")
                GameObject.Find("Orc Town Hall").GetComponent<townHall>().buildings -= 1;
            else
                GameObject.Find("Human Town Hall").GetComponent<townHall>().buildings -= 1;
            Destroy(gameObject);
        }
    }
}
