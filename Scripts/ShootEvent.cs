using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class ShootEvent : MonoBehaviour
{
    private bool gameStart;
    public float timeVal;
    public float targetShowTime;
    public float startTime;
    public int hitScore;
    public GameObject[] points;
    private Image[] imgPoints;
    



    /*
   void Start()
   {
       timeVal = 10; //十秒游戏
       targetShowTime = 0;
       hitScore = 0;
       gameStart = true;
   }

   // Update is called once per frame
   void Update()
   {
       if (gameStart)
       {
           if (timeVal <= 0)
           {
               gameStart = false;
               if (hitScore >= 3)
               {
                   Debug.Log("game winner, the score is " + hitScore);
               }
               else
               {
                   Debug.Log("game loser, the score is" + hitScore);
               }
               gameObject.SetActive(false);

           }
           else
           {
               timeVal -= Time.deltaTime;
               if (targetShowTime <= 0)
               {
                   ShowPoints();
                   targetShowTime = 2;
               }
               else
               {
                   targetShowTime -= Time.deltaTime;
               }

           }
       }
    }


       public void ShowPoints()
       {
           points[Random.Range(0, points.Length)].SetActive(true);
       }


       public void HitPoint(GameObject target)
       {
           //点击后得分
           target.SetActive(false);
           hitScore++;
          //点击后立刻出现新的目标
           ShowPoints();
           targetShowTime = 2;
   }
   */


    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;
        startTime = Time.time;

    }

    void Update()
    {
        if(gameStart){

          if(Time.time - startTime >= 10){
            gameStart = false;

            if(hitScore >= 3){
                    Debug.Log("game winner, the score is " + hitScore);
                    GameManager.Instance.LoadNextScript(2); //剧本+2
            }
            else{
                    Debug.Log("game loser, the score is" + hitScore);
                    GameManager.Instance.LoadNextScript();//剧本+1
                }
            gameObject.SetActive(false);
          }

          else{
            timeVal -= Time.deltaTime;
            if(timeVal <= 0){
              ShowPoints();
              timeVal = 2;
            }
          }
        }
      }
    
    private void ShowPoints()
   {
        /*
        for (int i = 0; i < points.Length; i++)
       {
           points[i].SetActive(false);
       }
        
       GameObject point= points[Random.Range(0, points.Length)];
       point.SetActive(true);
        */
        GameObject point = points[Random.Range(0, points.Length)];
        point.SetActive(true);
       // points[Random.Range(0, points.Length)].SetActive(true);
        
        Image[] images = point.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g
                , images[i].color.b,0);
        }
       
        GameManager.Instance.DoShowOrHideUITween(true,false,0.5f,images);
   }
    

    public void HitPoint(GameObject obj)
   {
       AudioManager.Instance.PlaySound("Hit");
       hitScore++;
       obj.SetActive(false);
   }
  

}
