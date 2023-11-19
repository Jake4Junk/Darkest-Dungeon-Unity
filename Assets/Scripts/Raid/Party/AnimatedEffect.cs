using UnityEngine;
using Spine.Unity;
public class AnimatedEffect : MonoBehaviour
{
    [SerializeField]
    private BoneFollower follower;
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    [SerializeField]
    private Transform currentTransform;

    public SkeletonAnimation SkeletonAnimation { get { return skeletonAnimation; } }
    public int sortingOrder;
    public bool IsLooping { private get; set; }

    private FormationUnit followUnit;
    private string followBoneName;

    private void Start()
    {
        if(IsLooping == false)
            Destroy(gameObject, SkeletonAnimation.state.GetCurrent(0).Animation.Duration);
    }

    private void LateUpdate()
    {
        if (follower == null || followUnit == null)
            return;

        if (follower.SkeletonRenderer == null || follower.SkeletonRenderer.gameObject.activeSelf == false)
        {
            follower.SkeletonRenderer = followUnit.CurrentState;

            follower.followBoneRotation = false;
            follower.followZPosition = true;
            follower.bone = followUnit.CurrentState.Skeleton.FindBone(followBoneName);
            SkeletonAnimation.GetComponent<SkeletonRenderer>().sortingOrder = followUnit.CurrentState.GetComponent<SkeletonRenderer>().sortingOrder + 1;
        }
    }

    public void BindToTargetUnit(FormationUnit unit, SkeletonAnimation effectAnimation, string boneName)
    {
        followUnit = unit;
        followBoneName = boneName;

        gameObject.SetActive(true);
        follower.SkeletonRenderer = effectAnimation;
        currentTransform.SetParent(unit.RectTransform, false);

        follower.followBoneRotation = false;
        follower.followZPosition = true;
        follower.bone = effectAnimation.Skeleton.FindBone(boneName);
        follower.DoUpdate();
        follower.enabled = false;
        Destroy(follower);

        SkeletonAnimation.GetComponent<SkeletonRenderer>().sortingOrder = effectAnimation.GetComponent<SkeletonRenderer>().sortingOrder + 1;
    }

    public void BindToTarget(FormationUnit unit, SkeletonAnimation effectAnimation, string boneName)
    {
        followUnit = unit;
        followBoneName = boneName;

        gameObject.SetActive(true);
        follower.SkeletonRenderer = effectAnimation;

        follower.followBoneRotation = false;
        follower.followZPosition = true;
        follower.bone = effectAnimation.Skeleton.FindBone(boneName);
        SkeletonAnimation.GetComponent<SkeletonRenderer>().sortingOrder = effectAnimation.GetComponent<SkeletonRenderer>().sortingOrder + 1;
        currentTransform.SetParent(unit.RectTransform, false);
    }

    public void BindToTarget(RectTransform rect, SkeletonAnimation effectAnimation, string boneName)
    {
        gameObject.SetActive(true);
        follower.SkeletonRenderer = effectAnimation;

        follower.followBoneRotation = false;
        follower.followZPosition = true;
        follower.bone = effectAnimation.Skeleton.FindBone(boneName);
        SkeletonAnimation.GetComponent<SkeletonRenderer>().sortingOrder = effectAnimation.GetComponent<SkeletonRenderer>().sortingOrder + 1;
        currentTransform.SetParent(rect, false);
    }
}
