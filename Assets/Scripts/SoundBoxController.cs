using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoxController : MonoBehaviour
{
    public AudioSource chickenDead;
    public AudioSource carHonk;
    public AudioSource lightning;
    public AudioSource metalHit;
    public AudioSource star;
    public AudioSource swallow;
    public AudioSource zombie;
    public AudioSource zombieDead;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayChickenDead(){
        chickenDead.Play();
    }

    public void PlayCarHonk(){
        carHonk.Play();
    }

    public void PlayLightning(){
        lightning.Play();
    }

    public void PlayMetalHit(){
        metalHit.Play();
    }

    public void PlayStar(){
        star.Play();
    }

    public void PlaySwallow(){
        swallow.Play();
    }

    public void PlayZombie(){
        zombie.Play();
    }                

    public void PlayZombieDead(){
        zombieDead.Play();
    }                    
}
