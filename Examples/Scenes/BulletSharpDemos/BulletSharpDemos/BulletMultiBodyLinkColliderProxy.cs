using BulletSharp;
using BulletUnity;
using UnityEngine;

public class BulletMultiBodyLinkColliderProxy : MonoBehaviour
{
    public MultiBodyLinkCollider target;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        Matrix4x4 m = target.WorldTransform.ToUnity();
        transform.position = BSExtensionMethods2.ExtractTranslationFromMatrix(ref m);
        transform.rotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref m);
        transform.localScale = BSExtensionMethods2.ExtractScaleFromMatrix(ref m);
    }
}
