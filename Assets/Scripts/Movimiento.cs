using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Movimiento : MonoBehaviour
{
    private Vector3 targetPosition;
    private float giroY;
    private Vector3 posicionRaton;
    private Vector3 b;
    private float velocidad = 1;
    private Vector3 antitrigger;
    private RaycastHit2D rayo;
    /*void Start()
    {
        targetPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Physics.gravity = new Vector3(0, 0, 0);
    }*/
    /*void Update()
    {
        posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position != targetPosition)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                posicion();
                velocidad = 2f;
                giroPersonaje();
                
            }
            

            //gameObject.GetComponent<Rigidbody>().AddForce( /*Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5 * velocidad)*///);
        /*}
        else
        {
            velocidad = 1;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                posicion();
                
            }
            else
            {
                if(Mathf.Abs(posicionRaton.x - gameObject.transform.position.x) >= 0.5f || Mathf.Abs(posicionRaton.y - gameObject.transform.position.y + 0.5f) >= 0.5f) giroPersonaje();
            }
        }
        //gameObject.GetComponent<CharacterController>().Move(Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5 * velocidad));
        gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5 * velocidad);
        a();
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5 * velocidad);
    }*/
    /*private void FixedUpdate() {
        /*gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().position + Vector3.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * 5 * velocidad);
    }*/
    void giroPersonaje()
    {
        if (posicionRaton.x > transform.position.x)
        {
            giroY = 270 - (Mathf.Rad2Deg * (Mathf.Atan((posicionRaton.y - transform.position.y + 0.5f) / (posicionRaton.x - transform.position.x))));
        }
        if (posicionRaton.x < transform.position.x)
        {
            giroY = -(270 + Mathf.Rad2Deg * (Mathf.Atan((posicionRaton.y - transform.position.y + 0.5f) / (posicionRaton.x - transform.position.x))));
        }
        if (posicionRaton.x == transform.position.x)
        {
            if((posicionRaton.y + 0.5f) != transform.position.y) giroY = ((posicionRaton.y + 0.5f) > transform.position.y) ? 180f : 0f;
        }
        transform.rotation = Quaternion.AngleAxis(-50, new Vector3(1, 0, 0)) * Quaternion.AngleAxis(giroY, Vector3.up);
    }
    private void posicion(){

        //Calculo sin colisiones
        targetPosition = posicionRaton;
        targetPosition = new Vector2(targetPosition.x, targetPosition.y + 0.5f);
        Vector3 aux = targetPosition;
        Vector3.Normalize(aux);
        //Calculo con colisiones
        //Debug.Log("antes rayo" + aux);
                Ray rayo = new Ray(gameObject.transform.position, aux - gameObject.transform.position);
                RaycastHit hitObserver;
                //Debug.DrawRay(gameObject.transform.position, aux - gameObject.transform.position);
                Physics.Raycast(rayo, out hitObserver, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
                if(hitObserver.collider != null){
                    if(Vector3.Distance(gameObject.transform.position, targetPosition) > Vector3.Distance(gameObject.transform.position, hitObserver.point) && !hitObserver.collider.isTrigger){
                    targetPosition.x = hitObserver.point.x - rayo.direction.x * 0.1f;
                    targetPosition.y = hitObserver.point.y - rayo.direction.y * 0.1f;
                    //targetPosition = hitObserver.point + rayo.direction;
                    }
                }
                //Debug.Log("tras rayo" + targetPosition + " || " + hitObserver.point);
    }


    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<TriggerEvents>().changeScene(this.gameObject);
    }
    private void a() {
        if(gameObject.scene.isLoaded && SceneManager.sceneCount == 1){
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            gameObject.transform.position = Vector3.zero;
            targetPosition = Vector3.zero;
        }
    }
    /*private void OnTriggerStay(Collider other) {
        transform.position = new Vector3(gameObject.transform.position.x - other.gameObject.transform.position.x, gameObject.transform.position.y - other.gameObject.transform.position.y, gameObject.transform.position.z);
        targetPosition = gameObject.transform.position;
        //targetPosition = new Vector3(gameObject.transform.position.x - other.gameObject.transform.position.x, gameObject.transform.position.y - other.gameObject.transform.position.y, gameObject.transform.position.z);
        /*if(Mathf.Abs(targetPosition.x - other.transform.position.x) > 0.5f || Mathf.Abs(targetPosition.y - other.transform.position.y) > 0.5f){
        
        }else{
            targetPosition = antitrigger;
        }
        
        Debug.Log(other.gameObject.name);
        Debug.Log("eheoihgre");
    }*/
    public void setTargetPosition(Vector3 providedTargetPosition){
        this.targetPosition = providedTargetPosition;
    }
}