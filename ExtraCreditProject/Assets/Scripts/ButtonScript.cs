using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public GameObject LoadResourcePanel;
    public GameObject LoadURLPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPanel(string name)
    {
        if(name == "resourcePanel")
        {
            LoadResourcePanel.SetActive(true);

        } else if(name == "urlPanel")
        {
            LoadURLPanel.SetActive(true);
        }
    }

    public void HidePanel(string name)
    {
        if(name == "resourcePanel")
        {
            LoadResourcePanel.SetActive(false);

        } else if(name == "urlPanel")
        {
            LoadURLPanel.SetActive(false);
        }


    }
}
