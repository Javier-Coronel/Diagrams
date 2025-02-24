using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    Texture2D map;
    public Image imagen;
    // Start is called before the first frame update
    void Start()
    {
        map = new Texture2D(8,8);
        map.filterMode = FilterMode.Point;
        map.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerPos=GameDataModification.Instance.FindPositionOfActualDioram();
        for(int i = 0;i<map.width;i++){
            for(int j = 0;j<map.width;j++){
                if (i == playerPos.x && j == playerPos.y)
                {
                    map.SetPixel(i, j, Color.red);
                    continue;
                }
                map.SetPixel(i,j,Color.white);
                
            }
        }
        map.Apply();
        imagen.sprite=Sprite.Create(map,new Rect(0,0,map.width,map.height),Vector2.zero);
    }
}
