using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartsChecklist : MonoBehaviour
{
    public GameObject puzzleParts;
    private List<GameObject> parts = new List<GameObject>();
    RectTransform sidebar;
    public List<Image> partsImages = new List<Image>(); 
    //float elapsedFrames = 0;
    //float elapsedFramesBack = 0;
    //float sidebarFrames = 500;
    //float time = 3f;
    //float timePassed;
    //public bool endAnimation = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sidebar = GetComponent<RectTransform>();

        for(int i=0; i<puzzleParts.transform.childCount; i++)
        {
            parts.Add(puzzleParts.transform.GetChild(i).gameObject);
        }

        for(int i=0; i<transform.childCount; i++)
        {
            partsImages.Add(transform.GetChild(i).GetComponent<Image>());
            if(i >= puzzleParts.transform.childCount)
            {
                partsImages[i].color= Color.clear;
            }
            else
            {
                partsImages[i].sprite = parts[i].GetComponent<SpriteRenderer>().sprite;
                partsImages[i].color = Color.black;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*public void checklistAnimation()
    {   

        float animationRatio = elapsedFrames / sidebarFrames;

        sidebar.anchoredPosition = new Vector2(Mathf.Lerp(100, 0, animationRatio), 0);
        elapsedFrames = (elapsedFrames + 1) % sidebarFrames;


        if(sidebar.anchoredPosition.x == 0)
        {
            // wait 10 seconds
            timePassed += Time.deltaTime;
            if(timePassed >= time)
            {   
                animationRatio = elapsedFramesBack / sidebarFrames;
                sidebar.anchoredPosition = new Vector2(Mathf.Lerp(0, 100, animationRatio), 0);
                elapsedFramesBack = (elapsedFramesBack + 1) % sidebarFrames;
                if(sidebar.anchoredPosition.x == 100)
                {
                    timePassed = 0;
                    endAnimation = true;
                }
            }       
        }
    }*/

}
