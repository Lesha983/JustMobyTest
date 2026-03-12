namespace JustMobyTest.Gameplay
{
    using DG.Tweening;
    using Pools;
    using TMPro;
    using UnityEngine;

    public struct DamageTextInfo : IReinitializingInfo
    {
        public Vector3 Position;
        public float Amount;
    }
    
    public class DamageText : CustomPoolable<DamageTextInfo>
    {
        [SerializeField]
        private TextMeshPro textMesh;
        [SerializeField]
        private float duration;
        [SerializeField]
        private Vector3 offset;
        
        public override void Reinitialize(DamageTextInfo info)
        {
            textMesh.alpha = 1f;
            textMesh.text = info.Amount.ToString();
            textMesh.transform.position = info.Position + offset;
            
            ShowAnimation();
            DOVirtual.DelayedCall(duration * 0.4f, HideAnimation, false).SetLink(gameObject);
        }

        private void ShowAnimation()
        {
            transform.DOMove(transform.position + Vector3.up * 0.3f, duration)
                .SetEase(Ease.OutExpo)
                .SetLink(gameObject);
        }
        
        private void HideAnimation()
        {
            textMesh.DOFade(0f, duration - duration * 0.4f)
                .SetLink(gameObject)
                .OnComplete(Despawn);
        }
    }
}