using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class BallController : MonoBehaviour
{
   
    [SerializeField] private float thrust = 250f;
    
    [SerializeField] private Rigidbody rb;

   
    [SerializeField] private float wallDistance = 5f;

   
    [SerializeField] private float minCamDistance = 5f;

    
    private Vector2 lastMousePos;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Game sadece bir kez başlatılsın diye tutulan basit bir sayaç 
    int birkez = 0;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;

        
        if (Input.GetMouseButton(0))
        {
            if (birkez == 0) 
            {
                GameManager.singleton.StartGame();
                birkez++;
            } 

            Vector2 currentMousePos = Input.mousePosition;

            if (lastMousePos == Vector2.zero)
            {
                lastMousePos = currentMousePos;
            }

            deltaPos = currentMousePos - lastMousePos;

            lastMousePos = currentMousePos;

           
            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;

           
            rb.AddForce(force);
        }

        else
        {
            lastMousePos = Vector2.zero;
        }
    }

   
    private void FixedUpdate()
    {
        if (GameManager.singleton.GameEnded)
        {
            return;
        }
        if (GameManager.singleton.GameStarted)
        {
            rb.MovePosition(transform.position + Vector3.forward * 5 * Time.fixedDeltaTime); 
        }
    }

 
    private void LateUpdate()
    {
        
        Vector3 pos = transform.position;

      

        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }

      
        if (transform.position.z < Camera.main.transform.position.z + minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
            transform.position = pos;
        }
        
        transform.position = pos;
    }

    int beyazcarpisma = 0;
    int saricarpisma = 0;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Zemin")
        {
            if(collision.gameObject.tag == "Death")
            {
                saricarpisma++;
                Debug.Log("Sarıya " + saricarpisma + " kez çarptı.");
            }else
            {
                beyazcarpisma++;
                Debug.Log("Beyaza " + beyazcarpisma + " kez çarptı.");
            }
        }

        if (GameManager.singleton.GameEnded)
        {
            return;
        }

        if (collision.gameObject.tag == "Death")
        {
            GameManager.singleton.EndGame(false);
        }
    }
}