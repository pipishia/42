using UnityEngine;

public class Attack : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    public Transform attacktransform;

    void Start()
    {
        // 獲取母物體 SpriteRenderer 
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        attacktransform=this.transform;

    }

    void Update()
    {
        if (parentSpriteRenderer != null)
        {
            // 檢測母物體的 SpriteRenderer 的 flipX 是否為 true
            if (parentSpriteRenderer.flipX)
            {
                attacktransform.rotation=Quaternion.Euler(0,180,0);
            }
            else
            {
                attacktransform.rotation=Quaternion.Euler(0,0,0);
            }
        }
    }
}