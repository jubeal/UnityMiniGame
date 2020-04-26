using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHealth
{
    float CurrentHealth { get; set; }
}

public class CameraScript : MonoBehaviour
{
    private List<GameObject> selected = new List<GameObject>();
    private Vector3 mousePosition;
    private RaycastHit2D hit;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey("left ctrl"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ((hit = Physics2D.Raycast(ray.origin, ray.direction)) && hit.transform.tag == "Selectable")
                selected.Add(hit.transform.gameObject);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            selected.Clear();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ((hit = Physics2D.Raycast(ray.origin, ray.direction)) && hit.transform.tag == "Selectable")
                selected.Add(hit.transform.gameObject);
        }


        if (Input.GetMouseButtonDown(1) && selected.Count != 0)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ((hit = Physics2D.Raycast(ray.origin, ray.direction)) && hit.transform.tag == "Orc")
            {
                for (int i = 0; i < selected.Count; i++)
                {
                    selected[i].GetComponent<CaracterScript>().Attack(hit.transform);
                    selected[i].GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                for (int i = 0; i < selected.Count; i++)
                {
                    selected[i].GetComponent<CaracterScript>().ennemi = null;
                    selected[i].GetComponent<CaracterScript>().Walk(mousePosition);
                    selected[i].GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
