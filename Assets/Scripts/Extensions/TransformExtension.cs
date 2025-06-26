using UnityEngine;

namespace Extensions
{
    public static class TransformExtension
    {
        public static void SetParent(this Transform transform, Transform parent, Vector3 localPosition)
        {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
        }
    }
}
