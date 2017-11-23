using BulletSharp.Math;

namespace BulletSharp
{
	public interface ICharacterController : IAction
	{
        bool CanJump { get; }
        bool OnGround { get; }

        void Jump();
        void Jump(Vector3 dir);
        void PlayerStep(CollisionWorld collisionWorld, float deltaTime);
        void PreStep(CollisionWorld collisionWorld);
        void Reset(CollisionWorld collisionWorld);
        void SetUpInterpolate(bool value);
        void SetVelocityForTimeInterval(ref Vector3 velocity, float timeInterval);
        void SetWalkDirection(ref Vector3 walkDirection);
        void Warp(ref Vector3 origin);
	}
}
