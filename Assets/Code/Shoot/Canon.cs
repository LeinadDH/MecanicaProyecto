using UnityEngine;

public class Canon : MonoBehaviour
{
    [HideInInspector]public Vector2 launchDir;
    public float launchSpeed;

    
    public void Update()
    {
        launchDir = new Vector2 (this.transform.rotation.z, -0.5f);
    }

}
