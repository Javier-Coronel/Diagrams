using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataModification : MonoBehaviour
{
    // TODO : Hacer otra version de este sistema que en vez de funcionar asi funcione de la siguiente forma:
    // Hacer 4 diccionarios, uno para cada circulo (externo, intermedio, interior, sol)
    // Cada diccionario sera de clave int valor string.
    // Cuando se mueve a otro diorama, se tiene que cambiar al diorama que este en la clave que 

    public static GameDataModification Instance;
    /// <summary>
    /// Todos los dioramas representados en texto.
    /// </summary>
    public GameObject[,] dioaramas = new GameObject[8, 8];
    public GameObject actualDioram;
    public List<GameObject> dioramasOnList;
    public int ciclo = 1;
    public float timeToNextCicle = 30;
    private float timerToCicle = 0;
    public int internalZoneCicleRotation = 2, midleZoneCicleRotation = 4, externalZoneCicleRotation = 6;
    //Hay que cambiar esto a algo mas legible
    /// <summary>
    /// Este array representa una direccion de uno de los anillos, 
    /// </summary>
    private int[] 
    internalZonePositionsX = {2,2,2,2,3,4,5,5,5,5,4,3},
    internalZonePositionsY = {2,3,4,5,5,5,5,4,3,2,2,2},

    midleZonePositionsX    = {1,1,1,1,1,1,2,3,4,5,6,6,6,6,6,6,5,4,3,2},
    midleZonePositionsY    = {1,2,3,4,5,6,6,6,6,6,6,5,4,3,2,1,1,1,1,1},
    
    externalZonePositionsX = {0,0,0,0,0,0,0,0,1,2,3,4,5,6,7,7,7,7,7,7,7,7,6,5,4,3,2,1},
    externalZonePositionsY = {0,1,2,3,4,5,6,7,7,7,7,7,7,7,7,6,5,4,3,2,1,0,0,0,0,0,0,0};
    private Vector2Int[] transformToVector2Int(int[] x, int[] y)
    {
        Vector2Int[] a =new Vector2Int[Mathf.Min(x.Length,y.Length)];
        for(int i = 0; i < a.Length; i++)
        {
            a[i] = new Vector2Int(x[i],y[i]);
        }
        return a;
    }
    /// <summary>
    /// Initialized on Start
    /// </summary>
    private Vector2Int[]
        internalZonePositions = { },
        midleZonePositions = { },
        externalZonePositions = { };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        internalZonePositions = transformToVector2Int(internalZonePositionsX, internalZonePositionsY);
        midleZonePositions    = transformToVector2Int(midleZonePositionsX, midleZonePositionsY);
        externalZonePositions = transformToVector2Int(externalZonePositionsX, externalZonePositionsY);
        for (int i = 0; i < dioramasOnList.Count; i++)
        {
            dioaramas[i / dioaramas.GetLength(0), i % dioaramas.GetLength(1)] = dioramasOnList[i];
        }
        DebugDioramas();
        actualDioram= FindObjectsOfType<DioramaData>(false)[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timerToCicle += Time.deltaTime;
        if (timerToCicle >= timeToNextCicle)
        {
            ciclo++;
            timerToCicle = 0;
            if (ciclo % internalZoneCicleRotation == 0)
            {
                RotateCicle(1,1);
            }
            if (ciclo % midleZoneCicleRotation == 0)
            {
                RotateCicle(2,-1);
            }
            if (ciclo % externalZoneCicleRotation == 0)
            {
                RotateCicle(3,1);
            }
        }
    }
    
    void RotateCicle(int zone, int orientation){
        if(zone==1){
            for (int i = (orientation<0)?internalZonePositionsX.Length-1:1; (orientation<0)?(i>0):(i < internalZonePositionsX.Length); i += (orientation < 0) ? -1 : 1)
            {
                (dioaramas[internalZonePositions[i].x, internalZonePositions[i].y],dioaramas[internalZonePositions[0].x, internalZonePositions[0].y])=
                (dioaramas[internalZonePositions[0].x, internalZonePositions[0].y],dioaramas[internalZonePositions[i].x, internalZonePositions[i].y]);
            }

            
        }else if(zone==2){
            for (int i = (orientation < 0) ? midleZonePositionsX.Length-1 : 1; (orientation < 0) ? (i > 0) : (i < midleZonePositionsX.Length); i+= (orientation < 0) ? -1:1)
            {
                //Debug.Log(dioaramas[midleZonePositionsX[i], midleZonePositionsY[i]] + " " + dioaramas[midleZonePositionsX[0], midleZonePositionsY[0]]+" "+ midleZonePositionsX[i]+" "+ midleZonePositionsY[i]);
                (dioaramas[midleZonePositions[i].x, midleZonePositions[i].y], dioaramas[midleZonePositions[0].x, midleZonePositions[0].y]) =
                (dioaramas[midleZonePositions[0].x, midleZonePositions[0].y], dioaramas[midleZonePositions[i].x, midleZonePositions[i].y]);
            }
        }
        else if(zone==3){
            for (int i = (orientation < 0) ? externalZonePositionsX.Length-1 : 1; (orientation < 0) ? (i > 0) : (i < externalZonePositionsX.Length); i += (orientation < 0) ? -1 : 1)
            {
                (dioaramas[externalZonePositions[i].x, externalZonePositions[i].y], dioaramas[externalZonePositions[0].x, externalZonePositions[0].y]) =
                (dioaramas[externalZonePositions[0].x, externalZonePositions[0].y], dioaramas[externalZonePositions[i].x, externalZonePositions[i].y]);
            }
        }
        DebugDioramas();
    }
    /// <summary>
    /// Imprime el array bidimensional donde estan todos los dioramas.
    /// </summary>
    void DebugDioramas()
    {
        string a = "";
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8;j++){
                a+=dioaramas[i,j].name+" ";
            }
            a += "\n";
        }
        Debug.Log(a.ToString());
    }
    /// <summary>
    /// Busca un diorama en el array de dioramas y devuelve su position (empezando por 0).
    /// </summary>
    /// <param name="dioram">El diorama a buscar</param>
    /// <returns>La posicion del diorama, si no lo encuentra devuelve la posicion x -1 y -1</returns>
    public Vector2Int FindPositionOfDioram(GameObject dioram){
        for (int i = 0; i < dioaramas.GetLength(0); i++){
            for (int j = 0; j < dioaramas.GetLength(1); j++)
            {
                if (dioaramas[i, j].name.Equals(dioram.name))
                {
                    return new Vector2Int(i,j);
                }
            }
        }
        return Vector2Int.one*-1;
    }
    public Vector2Int FindPositionOfActualDioram(){
        return FindPositionOfDioram(actualDioram);
    }
}
