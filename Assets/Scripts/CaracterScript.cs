using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterScript : MonoBehaviour, IHealth
{
    public Vector3 dest;
    public float speed;
    private int quarter = 0;
    private Animator anim;
    public float CurrentHealth { get; set; }
    public Transform ennemi = null;
    private float nextAttack = 7;
    private GameObject death;
    private GameObject clone;
    private bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
        anim = GetComponent<Animator>();
        CurrentHealth = 100;
        if (tag == "Selectable")
            death = GameObject.Find("Human Dead");
        else
            death = GameObject.Find("Orc Dead");
    }

    // Update is called once per frame
    void Update()
    {

        if (ennemi)
            Attack(ennemi);
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed);
            Nothing();
        }

        if (CurrentHealth <= 0)
        {
            clone = Instantiate(death, transform.position + new Vector3(0, 0, 2), Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);
        }
        nextAttack += Time.deltaTime;
    }

    public void Nothing()
    {
        if (transform.position == dest && move)
        {
            if (quarter == 0)
                anim.SetTrigger("nothingup");
            else if (quarter == 1)
                anim.SetTrigger("nothingupleft");
            else if (quarter == 2)
                anim.SetTrigger("nothingleft");
            else if (quarter == 3)
                anim.SetTrigger("nothingdownleft");
            else if (quarter == 4)
                anim.SetTrigger("nothingdown");
            else if (quarter == 5)
                anim.SetTrigger("nothingdownright");
            else if (quarter == 6)
                anim.SetTrigger("nothingright");
            else
                anim.SetTrigger("nothingupright");
            move = false;
        }
    }

    public void Walk(Vector3 goal)
    {
        quarter = CalculateQuarter(goal);
        dest = goal;
        if (quarter == 0)
            anim.SetTrigger("walkup");
        else if (quarter == 1)
            anim.SetTrigger("walkupleft");
        else if (quarter == 2)
            anim.SetTrigger("walkleft");
        else if (quarter == 3)
            anim.SetTrigger("walkdownleft");
        else if (quarter == 4)
            anim.SetTrigger("walkdown");
        else if (quarter == 5)
            anim.SetTrigger("walkdownright");
        else if (quarter == 6)
            anim.SetTrigger("walkright");
        else
            anim.SetTrigger("walkupright");
        move = true;
    }

    public void Attack(Transform target)
    {
        ennemi = target;
        float dist = Vector3.Distance(transform.position, target.position);
        int currentQuarter = CalculateQuarter(target.position);
        if (dist > -0.5 && dist < 0.5 && nextAttack > 2.5f)
        {
            dest = transform.position;
            quarter = currentQuarter;
            if (quarter == 0)
                anim.SetTrigger("attackup");
            else if (quarter == 1)
                anim.SetTrigger("attackupleft");
            else if (quarter == 2)
                anim.SetTrigger("attackleft");
            else if (quarter == 3)
                anim.SetTrigger("attackdownleft");
            else if (quarter == 4)
                anim.SetTrigger("attackdown");
            else if (quarter == 5)
                anim.SetTrigger("attackdownright");
            else if (quarter == 6)
                anim.SetTrigger("attackright");
            else
                anim.SetTrigger("attackupright");
            ennemi.GetComponent<IHealth>().CurrentHealth -= 25;
            Debug.Log(ennemi.name + " " + ennemi.GetComponent<IHealth>().CurrentHealth);
            move = false;
            nextAttack = 0;
        }
        else if (nextAttack > 2.5f && quarter != currentQuarter)
        {
            quarter = currentQuarter;
            Walk(target.position);
        }
        else if ((dist < -0.5 || dist > 0.5) && nextAttack > 2.5f)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }

    int CalculateQuarter(Vector3 target)
    {
        float angle = Vector3.SignedAngle(transform.position - new Vector3(transform.position.x + 1,
                transform.position.y, 0), transform.position - target, new Vector3(0, 0, -1));
        if (angle <= -67.5f && angle > -112.5f)
            return 0;
        else if (angle <= -112.5 && angle > -157.5)
            return 1;
        else if (angle <= -157.5 || angle > 157.5)
            return 2;
        else if (angle <= 157.5 && angle > 112.5)
            return 3;
        else if (angle <= 112.5f && angle > 67.5f)
            return 4;
        else if (angle <= 67.5 && angle > 22.5)
            return 5;
        else if (angle <= 22.5 && angle > -22.5)
            return 6;
        else
            return 7;
    }
}
