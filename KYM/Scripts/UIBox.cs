using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIBox : MonoBehaviour
{
    public GameObject button;
    public GameObject buttonClick;

    public Text percent;
    public Text loading;

    private bool isStartclick;


    private void Awake()
    {
        buttonClick.SetActive(false);
        loading.gameObject.SetActive(false);
        percent.gameObject.SetActive(false);
        isStartclick = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonClick.SetActive(true);
        button.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(other.GetComponent<UISelect>().IsSelect)
            {
                other.GetComponent<UISelect>().OKSelect();
                Click();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        buttonClick.SetActive(false);
        button.SetActive(true);
    }

    public void Click()
    {
        if(!isStartclick)
        {
            isStartclick = true;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        loading.gameObject.SetActive(true);
        percent.gameObject.SetActive(true);
        Scene originalScene = SceneManager.GetActiveScene();
        List<AsyncOperation> sceneLoads = new List<AsyncOperation>();
        for (int i = 3; i > 0; i--)
        {
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(i , LoadSceneMode.Additive);
            sceneLoading.allowSceneActivation = false;
            sceneLoads.Add(sceneLoading);
            //yield return new WaitForSeconds(5.0f);
        }

        bool isAllLoad = false;
        while (!isAllLoad)
        {
            float value = 0;
            for (int i = 0; i < sceneLoads.Count; i++)
            {
                value += sceneLoads[i].progress;
            }

            value = value / sceneLoads.Count;
            int v = (int)(value * 100);
            percent.text = v.ToString() + "%";

            isAllLoad = true;
            for (int i = 0; i < sceneLoads.Count; i++)
            {
                if (sceneLoads[i].progress < 0.9f)
                {
                    isAllLoad = false;
                    break;
                }
            }
            yield return null;
        }

        for (int i = 0; i < sceneLoads.Count; i++)
        { 
            sceneLoads[i].allowSceneActivation = true;
            while (!sceneLoads[i].isDone) { yield return null; }
        }

        AsyncOperation sceneunloading = SceneManager.UnloadSceneAsync(originalScene);
        while (!sceneunloading.isDone) { yield return null; }
    }

    IEnumerator LoadAsynchronously()
    {
        Scene s = SceneManager.GetActiveScene();

        loading.gameObject.SetActive(true);
        percent.gameObject.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            else
            {
                int i = (int)(operation.progress * 100);
                percent.text = i.ToString() + "%";
                Debug.Log(operation.progress);
            }
            yield return null;
        }

        //SceneManager.UnloadSceneAsync(s);



    }
}
