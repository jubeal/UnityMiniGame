using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnnemi : MonoBehaviour
{
    private GameObject townHall;
    private GameObject mainTarget;
    private GameObject target = null;
    private Collider2D col;
    private bool state = true;
    // Start is called before the first frame update
    void Start()
    {
        mainTarget = GameObject.Find("Human Town Hall");
        townHall = GameObject.Find("Orc Town Hall");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && state)
        {
            state = false;
            GetComponent<CaracterScript>().Attack(mainTarget.transform);
        }
        else if (target && !state)
        {
            state = true;
            GetComponent<CaracterScript>().Attack(target.transform);
        }

        if ((col = Physics2D.OverlapCircle(townHall.transform.position, 2.0f, 1 << LayerMask.NameToLayer("Human")))
                && col.gameObject != target)
        {
            state = false;
            target = col.gameObject;
        }
        else if (target == null && (col = Physics2D.OverlapCircle(transform.position, 2.0f,
                1 << LayerMask.NameToLayer("Human"))))
            target = col.gameObject;
    }
}
