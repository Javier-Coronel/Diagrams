using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataModification : MonoBehaviour
{
    public static GameDataModification Instance;
    public string[,] dioaramas = new string[8, 8]
    {
        {"A1","B1","C1","D1","E1","F1","G1","H1"},
        {"A2","B2","C2","D2","E2","F2","G2","H2"},
        {"A3","B3","C3","D3","E3","F3","G3","H3"},
        {"A4","B4","C4","D4","E4","F4","G4","H4"},
        {"A5","B5","C5","D5","E5","F5","G5","H5"},
        {"A6","B6","C6","D6","E6","F6","G6","H6"},
        {"A7","B7","C7","D7","E7","F7","G7","H7"},
        {"A8","B8","C8","D8","E8","F8","G8","H8"},
    };
    public int ciclo = 1;
    public float timeToNextCicle = 30;
    private float timerToCicle = 0;
    public int internalZoneCicleRotation = 2, midleZoneCicleRotation = 4, externalZoneCicleRotation = 6;
    //Hay que cambiar esto a algo mas legible
    private int[] 
    internalZonePositionsX={2,2,2,2,3,4,5,5,5,5,4,3},
    internalZonePositionsY={2,3,4,5,5,5,5,4,3,2,2,2},
    midleZonePositionsX={1,1,1,1,1,1,2,3,4,5,6,6,6,6,6,6,5,4,3,2},
    midleZonePositionsY={1,2,3,4,5,6,6,6,6,6,6,5,4,3,2,1,1,1,1,1},
    externalZonePositionsX={0,0,0,0,0,0,0,0,1,2,3,4,5,6,7,7,7,7,7,7,7,7,6,5,4,3,2,1},
    externalZonePositionsY={0,1,2,3,4,5,6,7,7,7,7,7,7,7,7,6,5,4,3,2,1,0,0,0,0,0,0,0};

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
                (dioaramas[internalZonePositionsX[i],internalZonePositionsY[i]],dioaramas[internalZonePositionsX[0],internalZonePositionsY[0]])=
                (dioaramas[internalZonePositionsX[0],internalZonePositionsY[0]],dioaramas[internalZonePositionsX[i],internalZonePositionsY[i]]);
            }

            
        }else if(zone==2){
            for (int i = (orientation < 0) ? midleZonePositionsX.Length-1 : 1; (orientation < 0) ? (i > 0) : (i < midleZonePositionsX.Length); i+= (orientation < 0) ? -1:1)
            {
                //Debug.Log(dioaramas[midleZonePositionsX[i], midleZonePositionsY[i]] + " " + dioaramas[midleZonePositionsX[0], midleZonePositionsY[0]]+" "+ midleZonePositionsX[i]+" "+ midleZonePositionsY[i]);
                (dioaramas[midleZonePositionsX[i], midleZonePositionsY[i]], dioaramas[midleZonePositionsX[0], midleZonePositionsY[0]]) =
                (dioaramas[midleZonePositionsX[0], midleZonePositionsY[0]], dioaramas[midleZonePositionsX[i], midleZonePositionsY[i]]);
            }
        }
        else if(zone==3){
            for (int i = (orientation < 0) ? externalZonePositionsX.Length-1 : 1; (orientation < 0) ? (i > 0) : (i < externalZonePositionsX.Length); i += (orientation < 0) ? -1 : 1)
            {
                (dioaramas[externalZonePositionsX[i], externalZonePositionsY[i]], dioaramas[externalZonePositionsX[0], externalZonePositionsY[0]]) =
                (dioaramas[externalZonePositionsX[0], externalZonePositionsY[0]], dioaramas[externalZonePositionsX[i], externalZonePositionsY[i]]);
            }
        }
        DebugDioramas();
    }
    void DebugDioramas()
    {
        string a = "";
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8;j++){
                a+=dioaramas[i,j]+" ";
            }
            a += "\n";
        }
        Debug.Log(a.ToString());
    }
}
