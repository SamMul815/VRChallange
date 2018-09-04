using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 파티클이 슬로우에 안걸리게 해주는 코드
/// Time.sclae값에 영향을 안받게 해준다.
/// </summary>
public class UnScaleTimeParticle : MonoBehaviour {

    ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
    }


    // Update is called once per frame
    void Update ()
    {
        particle.Simulate(Time.unscaledDeltaTime, true, false);
        //if(Time.timeScale < 0.5f)
        //{
        //    ParticleSystem[] ps= this.GetComponentsInChildren<ParticleSystem>();
        //    for(int i = 0; i<ps.Length; i++)
        //    {
        //        var main = ps[i].main;
        //        main.simulationSpeed = 1.0f;
        //    }

        //}
        //particle.main.simulationSpeed = 1;
        //particle.main.simulationSpeed = 1.0f;



        //particle
        //Debug.Log("time" + GetComponent<ParticleSystem>().time);
        //GetComponent<ParticleSystem>().

        //float deltaTime = Time.realtimeSinceStartup - lastTime;

        //lastTime = Time.realtimeSinceStartup;
        //particle.Simulate(Time.unscaledDeltaTime, true);
    }
    private void OnEnable()
    {
        particle.time = 0;
  
    }

}
