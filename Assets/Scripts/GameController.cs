using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GameController : MonoBehaviour
{
    private Camera      cam;
    public  Transform   PlayerTransform;

    public  float       speedCam;
    public  Transform   limiteCamEsq, limiteCamDir, limiteCamSup, limiteCamInf;

    [Header("Audio")]
    public AudioSource  sfx;
    public AudioSource  musicSource;

    public AudioClip    sfxJump;
    public AudioClip    sfxAttack;
    public AudioClip    sfxCoin;
    public AudioClip    sfxEnemyDead;
    public AudioClip[]  sfxStep;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        CamController();
    }

    private void CamController()
    {
        float posCamX = PlayerTransform.position.x;
        float posCamY = PlayerTransform.position.y;

        if (cam.transform.position.x < limiteCamEsq.position.x && PlayerTransform.position.x < limiteCamEsq.position.x)
        {
            posCamX = limiteCamEsq.position.x;
        }
        else if (cam.transform.position.x > limiteCamDir.position.x && PlayerTransform.position.x > limiteCamDir.position.x)
        {
            posCamX = limiteCamDir.position.x;
        }

        if (cam.transform.position.y < limiteCamInf.position.y && PlayerTransform.position.y < limiteCamInf.position.y)
        {
            posCamY = limiteCamInf.position.y;
        }
        else if (cam.transform.position.y > limiteCamSup.position.y && PlayerTransform.position.y > limiteCamSup.position.y)
        {
            posCamY = limiteCamSup.position.y;
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }

    public void PlaySfx(AudioClip sfxClip, float volume)
    {
        sfx.PlayOneShot(sfxClip, volume);
    }

}
