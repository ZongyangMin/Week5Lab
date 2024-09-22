using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    public bool gameOver = false;

    public int meteorCount = 0;
    public int prevMeteorCount;

    public AudioClip explosion;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMeteor", 1f, 2f);
        prevMeteorCount = meteorCount;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }

        if (meteorCount == 5)
        {
            BigMeteor();
            CinemaShake.Instance.ShakeCamera(5f, .1f);
        }
        if(meteorCount>prevMeteorCount)
        {
            audioSource.PlayOneShot(explosion,0.5f);
            prevMeteorCount = meteorCount;
        }
        else if(meteorCount == 0 && prevMeteorCount>0)
        {
            prevMeteorCount = meteorCount;
            audioSource.PlayOneShot(explosion,0.5f);
        }

    }

    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    void BigMeteor()
    {
        meteorCount = 0;
        Instantiate(bigMeteorPrefab, new Vector3(Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }
}
