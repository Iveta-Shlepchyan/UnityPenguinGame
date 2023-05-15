using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] SpriteShapeController spriteShapeController;

    [SerializeField, Range(3f, 100f)] int levelLength = 50;
    [SerializeField, Range(1f, 50f)] float xMultiplier = 2f;
    [SerializeField, Range(1f, 50f)] float yMultiplier = 2f;
    [SerializeField, Range(0f, 1f)] float curveSmoothness = 0.5f;

    [SerializeField] float noiseStep = 0.5f;
    [SerializeField] float bottom = 10f;

    [SerializeField] GameObject fish;
    GameObject[] fishes;

    Vector3 lastPos;

    void OnValidate() 
    {
        spriteShapeController.spline.Clear();

        for (int i = 0; i < levelLength; i++)
        {
            int random = Mathf.Abs(Random.Range(0, 80) - Random.Range(0,50));
            float random2 = lastPos.y + Random.Range(0,30) - Random.Range(0,50);
            lastPos = transform.position + new Vector3(i * xMultiplier - Random.Range(0, 30), Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier  + random);
            spriteShapeController.spline.InsertPointAt(i, lastPos);

            if(i != 0 && i != levelLength -1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }
        }


        spriteShapeController.spline.InsertPointAt(levelLength, new Vector3(lastPos.x, transform.position.y - bottom));
        spriteShapeController.spline.InsertPointAt(levelLength + 1, new Vector3(transform.position.x, transform.position.y - bottom));

    }

    void Start() 
    {
        //fishes = new GameObject[levelLength];
        AddFishPickups();
    }


    void AddFishPickups()
    {
        // if(fishes.Length > 0)
        // {
        //     foreach(GameObject fish in fishes)
        //     {
        //         DestroyImmediate(fish);
        //     }
        // }
        
        //int j = 0;
        for (int i = 1; i < spriteShapeController.spline.GetPointCount()-1; i+=2)
        {
            if(Random.Range(0,2)==1)
            {
                Vector3 spawnPlace = new Vector3(spriteShapeController.spline.GetPosition(i).x , spriteShapeController.spline.GetPosition(i).y + 1f);
                GameObject fishObj = Instantiate(fish, spawnPlace, Quaternion.identity);
                //fishes[j] = fishObj;
                //j++;
            }
            
        }

    }

}
