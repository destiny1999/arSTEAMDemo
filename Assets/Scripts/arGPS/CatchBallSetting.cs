using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBallSetting : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 shootPos = new Vector3();
    [SerializeField] Camera referenceCamera = null;
    Vector3 maxHeight = new Vector3();
    Vector3 minHeight = new Vector3();
    Vector3 originalHeight = new Vector3();
    Vector3 beforeShootPos = new Vector3();
    [SerializeField] float baseHeight = 500f;
    [SerializeField] float forceWeight = 10f;
    [SerializeField] PhysicMaterial bouncessPhysical = null;
    [SerializeField] Material catchingColor = null;
    
    bool throwball = false;
    bool collisioned = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDrag()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = this.transform.position.z;
        var pos = referenceCamera.ScreenToWorldPoint(mousePos);
        this.transform.position = pos;
        
        maxHeight = this.transform.position;
        if (this.transform.position.y < maxHeight.y)
        {
            minHeight = this.transform.position;
        }

    }

    private void OnMouseUp()
    {
        this.GetComponent<Collider>().material = bouncessPhysical;
        //mouseUp = true;
        
        shootPos = new Vector3(this.transform.position.x, this.transform.position.y - 500f, this.transform.position.z);

        float h = maxHeight.y - minHeight.y;
        float w = Mathf.Floor(h);

        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 1) * h * forceWeight);
        throwball = true;

    }
    private void OnMouseDown()
    {
        this.GetComponent<Collider>().material = null;
        originalHeight = this.transform.position;
        maxHeight = this.transform.position;
        minHeight = this.transform.position;
        //mouseUp = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collisioned)
        {
            if (throwball)
            {
                if (collision.transform.tag.Equals("pet"))
                {
                    StartCoroutine(IntoGetPet(collision.transform.gameObject));
                }
                else
                {
                    CatchSceneController.Instance.SpawnNewBall();
                    Destroy(this.gameObject);
                }
                collisioned = true;
            }
        }
    }
    IEnumerator IntoGetPet(GameObject pet)
    {
        this.GetComponent<Renderer>().material = catchingColor;
        pet.SetActive(false);
        this.GetComponent<Collider>().material = null;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        this.transform.localPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z);

        bool get = Random.Range(1, 101) <= 90 ? true : false;
        yield return (CatchingAni());
        if (get)
        {
            ChangeSceneManager.SetNextViewName("goMapLocation");
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadScene");
        }
        else
        {
            pet.SetActive(true);
            CatchSceneController.Instance.SpawnNewBall();
        }
        Destroy(this.gameObject);
    }
    IEnumerator CatchingAni()
    {
        float catchingTime = 2f;
        float time = 0;
        while(time < catchingTime)
        {
            time += Time.deltaTime * 1;
            yield return 1;
        }
    }
    public void SetReferenceCamera(Camera camera)
    {
        referenceCamera = camera;
    }
}
