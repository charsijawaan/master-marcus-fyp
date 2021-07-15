using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCar : MonoBehaviour
{

    private bool Triggered = false;
    public GameObject Car;
    public float CarSpeed = 100f;
    Vector3 startPositionOfCar;
    Vector3 endPositionOfCar;
    private CharacterControl MarcusControl;
    private DamageDetector MarcusDamageDetector;
    public RuntimeAnimatorController foo;

    public void Start()
    {
        startPositionOfCar = new Vector3(Car.transform.position.x, Car.transform.position.y, Car.transform.position.z);
        endPositionOfCar = new Vector3(startPositionOfCar.x + 40f, startPositionOfCar.y, startPositionOfCar.z);

        MarcusControl = CharacterManager.Instance.GetPlayableCharacter();
        MarcusDamageDetector = MarcusControl.GetComponent<DamageDetector>();
        //Debug.Log(Car.GetComponent<BoxCollider>().bounds.center.x + (Car.GetComponent<BoxCollider>().bounds.size.x / 2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Triggered)
        {
            //Debug.Log("Entered");
            // do nothing
        }

        Triggered = true;
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
    }

    private void Update()
    {            
        if (Triggered)
        {
            StartCar();
        }            
    }

    private void StartCar()
    {
        float CarBoxColliderX = Car.GetComponent<BoxCollider>().bounds.center.x + (Car.GetComponent<BoxCollider>().bounds.size.x / 2f);
        if (!(Car.transform.position.x > endPositionOfCar.x))
        {
            if(CarBoxColliderX + 4f >= MarcusControl.transform.position.x)
            {
                MarcusDamageDetector.TakeTotalDamage();

                MarcusControl.animationProgress.RagdollTriggered = true;
                MarcusControl.GetComponent<BoxCollider>().enabled = false;
                MarcusControl.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
                MarcusControl.RIGID_BODY.useGravity = false;

                //MarcusControl.SkinnedMeshAnimator.runtimeAnimatorController = foo;
            }
            Car.transform.position += new Vector3(2f, 0f, 0f) * Time.deltaTime * CarSpeed;
        }

    }

}

