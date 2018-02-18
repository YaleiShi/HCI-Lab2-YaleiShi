using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class FacialExpressions : MonoBehaviour {

    //Script containing levels of emotions
    ImageResultsParser userEmotions;
    Rigidbody rb; 
    //player Transform used to obtain reference to UserEmotions script
    Transform player;
    Transform mainCamera;

    public float speed = 1;
    public float maxHeight = 2.5f;
    public Renderer faceRenderer;

	private Material faceMaterial;
	private Vector2 uvOffset;
	private Animator animator;

    // Use this for initialization
    // Initialization and Setting references
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Sailor").transform;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;        
        userEmotions = player.GetComponent<ImageResultsParser>();
        rb = player.GetComponent<Rigidbody>();
        uvOffset = Vector2.zero;
        faceMaterial = faceRenderer.materials[1];
    }

    // Setting the user's dominant emotion every frame
    void Update()
    {
        // TODO for Question 2
        if(userEmotions.joyLevel > 15)
        {
            setJoyful();
        }
        else if(userEmotions.sadnessLevel > 15)
        {
            setSad();
        }else if(userEmotions.surpriseLevel > 15)
        {
            setSurprise();
        }
        else
        {
            setIdle();
        }
        
        faceMaterial.SetTextureOffset("_MainTex", uvOffset);
    }

    private void LateUpdate()
    {
        mainCamera.LookAt(player);
    }


    //sets the Character's emotion to Idle (Emotionless)
    void setIdle()
    {
        // TODO for Question 2
        uvOffset = new Vector2(0, 0);
        float height = player.position.y;
    
        if (height > 0)
        {
            //move down
            Vector3 movement = new Vector3(0, -0.05f * speed, 0);
            player.Translate(movement);
        }
    }

    //sets the Character's emotion to Joyful (Smiling)
    void setJoyful()
    {
        // TODO for Question 2
        float height = player.position.y;
        uvOffset = new Vector2(0.25f, 0);
        
        // if the character hasn't go over the maxHeight
        if(height <= maxHeight)
        {
            //move up, faster than the speed of moving down
            Vector3 movement = new Vector3(0, 0.05f * speed, 0);
            player.Translate(movement);
        }
        
    }

    //sets the Character's emotion to Sad (Frowning)
    void setSad()
    {
        // TODO for Question 2
        uvOffset = new Vector2(0, -0.25f);
        float height = player.position.y;
       
        if (height >= 0)
        {
            //if you are sad, the moving down speed is faster
            Vector3 movement = new Vector3(0, -0.125f * speed, 0);
            player.Translate(movement);
        }
    }

    //Question 4, new emotion-surprise, chracter should run in panic
    void setSurprise()
    {
        uvOffset = new Vector2(0.25f, -0.25f);
        //if character stand on the ground, run in panic
        if(player.position.y > -0.1 && player.position.y < 0.1)
        {
            transform.RotateAround(new Vector3(1, 0, 0), Vector3.up, 100 * Time.deltaTime);
        }    
    }

}
