using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameObject weaponPrefab;

    public float speed=5.0f;
    private int hasPowerUp = 0;
    private float additionPowerStrength = 1.0f;
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
        if(hasPowerUp>0)
        {
            PowerupAbility();
        }
        if(transform.position.y < -1)
        {
            Debug.Log("Game Over!");
            Clear();
        }
    }

    void Clear()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        hasPowerUp = 0;
        additionPowerStrength = 1.0f;
        powerupIndicator.gameObject.SetActive(false);
    }
    void PowerupAbility()
    {
        switch (hasPowerUp)
        {
            case 2:
                if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
                {
                    playerRb.AddForce(Vector3.up * speed,ForceMode.Impulse);
                    isOnGround = false;
                }
                break;      
                
            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < enemy.Length; i++)
                    {
                        Vector3 awayFromWeapon = (enemy[i].gameObject.transform.position - transform.position);
                        Vector3 weaponOffset = awayFromWeapon.normalized;
                        GameObject weapon = Instantiate(weaponPrefab,transform.position+weaponOffset,weaponPrefab.transform.rotation);             
                        weapon.GetComponent<Rigidbody>().AddForce(awayFromWeapon * additionPowerStrength, ForceMode.Impulse);
                    }
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            switch (other.GetComponent<PowerUp>().type)
            {
                
                case 1:
                    hasPowerUp = 1;
                    additionPowerStrength = 15.0f;
                    break;
                case 2:
                    hasPowerUp = 2;
                    additionPowerStrength = 5.0f;
                    break;
                case 3:
                    hasPowerUp = 3;
                    additionPowerStrength = 2.0f;
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidboy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidboy.AddForce(awayFromPlayer * additionPowerStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerUp);
        }
        else if (collision.gameObject.CompareTag("Ground") && !isOnGround)
        {
            isOnGround = true;
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            for(int i = 0; i < enemy.Length; i++)
            {
                Rigidbody enemyRigidboy = enemy[i].gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (enemy[i].gameObject.transform.position - transform.position);
                enemyRigidboy.AddForce(awayFromPlayer * additionPowerStrength, ForceMode.Impulse);
            }
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = 0;
        additionPowerStrength = 1.0f;
        powerupIndicator.gameObject.SetActive(false);
    }
}
