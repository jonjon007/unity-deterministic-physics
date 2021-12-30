using UnityEngine;
using UnityS.Mathematics;

public class ResourceManager : MonoBehaviour
{
    public Material defaultMaterial;
    public int seed;

    private static ResourceManager _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourceManager>();
            }

            return _instance;
        }
    }

    public static MaterialPropertyBlock materialPropertyBlock;

    private void Awake()
    {
        _instance = this;
        materialPropertyBlock = new MaterialPropertyBlock();
    }

    void Update(){
        // if(Input.GetKeyDown("a")){
        //     PhysicsController pc = PhysicsController.Instance;
        //     pc.ShootBall((sfloat)20.0f, (sfloat)12.0f, new float3((sfloat)50.0f, (sfloat)20.0f, (sfloat)(-50.0f)), new float3((sfloat)(-100.0f), (sfloat)1.0f, (sfloat)100.0f));
        //     Debug.Log("Ball shot");
        // }
    }
}
