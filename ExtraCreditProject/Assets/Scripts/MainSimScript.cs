using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainSimScript : MonoBehaviour {

    public GameObject plane;
    public Material floorMat;
    public GameObject blackPrefab;
    public GameObject whitePrefab;
    public Transform spawnPoint;

    private Texture2D texture;
    private List<Vector3> locations;
    private Vector3 lowerLeft, lowerRight, upperLeft, upperRight;

	// Use this for initialization
	void Start () {
        locations = new List<Vector3>(400);
        
        // Define plane corner coordinates.
        lowerLeft = new Vector3(-10f, 0f, -10f);
        upperLeft = new Vector3(-10f, 0f, 10f);
        upperRight = new Vector3(10f, 0f, 10f);
        lowerRight = new Vector3(10f, 0f, -10f);

    }

    void Update()
    {
        
        texture = (Texture2D)plane.GetComponent<Renderer>().material.mainTexture;
            

    }

    public void ChooseBlack()
    {

        if (texture == null)
        {
            return;
        }

        // Define variables to be used in processing image.
        Color c = new Color(0, 0, 0);

        float threshold = 0.1f;
        float max = Mathf.Clamp01(c.grayscale + threshold);
        float min = Mathf.Clamp01(c.grayscale - threshold);

        for(int y = 0; y < texture.height; y++)
        {

            for(int x = 0; x < texture.width; x++)
            {
                Color pixel = texture.GetPixel(x, y);

                if (pixel.grayscale >= min && pixel.grayscale <= max)
                {
                    HandlePixelLocation(x, y, c);
                }

            }
        }
    }

    public void ChooseWhite()
    {
        //texture = (Texture2D)plane.GetComponent<Renderer>().material.mainTexture;

        if (texture == null)
        {
            return;
        }

        // Define variables to be used in processing image.
        Color c = new Color(255, 255, 255);

        float threshold = 0.1f;
        float max = Mathf.Clamp01(c.grayscale + threshold);
        float min = Mathf.Clamp01(c.grayscale - threshold);

        for (int y = 0; y < texture.height; y++)
        {

            for (int x = 0; x < texture.width; x++)
            {
                Color pixel = texture.GetPixel(x, y);

                if (pixel.grayscale >= min && pixel.grayscale <= max)
                {
                    HandlePixelLocation(x, y, c);
                }

            }
        }
    }

    private void HandlePixelLocation(int x, int y, Color c)
    {

        float xFactor = (float)x / texture.width;
        float yFactor = (float)y / texture.height;

        Vector3 worldPoint = (1.0f - xFactor) * (1.0f - yFactor) * lowerLeft + (xFactor) * (1.0f - yFactor) * lowerRight + (xFactor) * (yFactor) * upperRight + (1.0f - xFactor) * (yFactor) * upperLeft;
        //Vector3 roundedPoint = new Vector3((int)worldPoint.x, (int)worldPoint.y, (int)worldPoint.z);
        Vector3 roundedPoint = new Vector3(Mathf.RoundToInt(worldPoint.x), worldPoint.y, Mathf.RoundToInt(worldPoint.z));
        if (!locations.Contains(roundedPoint))
        {
            locations.Add(roundedPoint);
            SpawnCharacter(roundedPoint, c);
        }
    }

    private void SpawnCharacter(Vector3 dest, Color c)
    {
        Color black = new Color(0, 0, 0);
        Color white = new Color(255, 255, 255);

        GameObject character;

        if (c == black)
        {
            character = Instantiate(blackPrefab);
            character.transform.position = spawnPoint.position;
            character.GetComponent<NavMeshAgent>().SetDestination(dest);
            return;

        } else if(c == white){

            character = Instantiate(whitePrefab);
            character.transform.position = spawnPoint.position;
            character.GetComponent<NavMeshAgent>().SetDestination(dest);
            return;

        }
    }

    public void Reset()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("character");
        for(int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }

        plane.GetComponent<Renderer>().material.mainTexture = floorMat.mainTexture;
    }

}
