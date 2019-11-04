using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitService : MonoBehaviour
{
    public Transform orbitingObject;
    public Transform SunModel;
    public Path orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    //public bool orbitActive = true;
    public float axisRotation = 0f;
    public float speed = 0f;

    [SerializeField]
    private float planetSpeed = 0.1f;

    public GameObject planetDetailPanel;
    string[] planets = new string[] { "Mercury", "Venus", "Earth" };
    public Text planetName;

    private void Start()
    {
        planetDetailPanel.SetActive(false);
        
        //if (orbitingObject == null)
        //{
        //    orbitActive = false;
        //    return;
        //}
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateObject());
    }


    void SetOrbitingObjectPosition()
    {
        Vector3 objPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition=new Vector3(objPos.x,objPos.z,objPos.y);
        orbitingObject.transform.Rotate(0f, axisRotation, 0f, Space.Self);
    }

    IEnumerator AnimateObject()
    {
        if(orbitPeriod<0.1f)
        {
            orbitPeriod = 0.1f;
        }
        // planetsDistance = Vector3.Distance(orbitingObject.position, SunModel.position);
        
        while (true)
        {
            //if (Vector3.Distance(orbitingObject.position, SunModel.position) < 5f)
            //{
            //float orbitSpeed = 1f / orbitPeriod;
            //OrbitSpeedValue(orbitSpeed);
            //planetSpeed = orbitSpeed;
            //var planetsDistance = orbitingObject.localPosition - SunModel.position;
            //var distance = planetsDistance.magnitude;
            //var normalized = planetsDistance / distance;
            //float direction = normalized.x;

            float planetsDistance = Vector3.Distance(orbitingObject.position, SunModel.position);
            float normalize = 1f / planetsDistance;

            if (orbitingObject.position.z > 0)
            {
                float orbitSpeed = (normalize*0.3f) / orbitPeriod;
                //Debug.Log(orbitSpeed);
                //Debug.Log(planetsDistance);
                //Debug.Log(distance);
                orbitProgress += Time.deltaTime * orbitSpeed;
                orbitProgress %= 1f;

            }
            else
            {
                float orbitSpeed = planetSpeed / orbitPeriod; // 1f/orbit period
                //Debug.Log(orbitSpeed);
                //Debug.Log(planetsDistance);
                //Debug.Log(distance);
                orbitProgress += Time.deltaTime * orbitSpeed;
                orbitProgress %= 1f;
            }
            SetOrbitingObjectPosition();
            yield return null;
            //}
            //

        }
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 0;
                planetDetailPanel.SetActive(true);
                for (int i = 0; i < 10; i++)
                {
                    if (hit.transform.name == "Planet" + i)
                    {
                        planetName.text = planets[i];
                        Debug.Log(hit.transform.name);
                    }
                }
            }
        }
    }

    public void Back()
    {
        Time.timeScale = 1;
        planetDetailPanel.SetActive(false);
    }
    
}
