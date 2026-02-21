using UnityEngine;

public class ParallaxSky : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject cloudOne;
    [SerializeField] GameObject cloudTwo;
    [SerializeField] float parallaxSpeed;
    [SerializeField] Transform baloonTransform;

    private void Start()
    {
        cloudOne.transform.position = Vector3.zero;
        cloudTwo.transform.position = new Vector3(0,8f,0);
    }
    private void Update()
    {
        if (GameManager.Instance!=null &&  GameManager.Instance.isGameStopped() ) return;
        cloudOne.transform.position += Vector3.down * parallaxSpeed;
        cloudTwo.transform.position += Vector3.down * parallaxSpeed;

        if(cloudOne.transform.position.y <= baloonTransform.position.y-4)
        {
            cloudOne.transform.position = new Vector3(0, 8f, 0);
        }
        else if (cloudTwo.transform.position.y <= baloonTransform.position.y-4)
        {
            cloudTwo.transform.position = new Vector3(0, 8f, 0);
        }
    }

}
