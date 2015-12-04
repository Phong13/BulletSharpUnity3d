using System;
using BulletSharp.Math;

namespace BulletSharp
{
	public struct WheelInfoConstructionInfo
	{
        public bool IsFrontWheel;
        public Vector3 ChassisConnectionCS;
        public float FrictionSlip;
        public float MaxSuspensionForce;
        public float MaxSuspensionTravelCm;
        public float SuspensionRestLength;
        public float SuspensionStiffness;
        public Vector3 WheelAxleCS;
        public Vector3 WheelDirectionCS;
        public float WheelRadius;
        public float WheelsDampingCompression;
        public float WheelsDampingRelaxation;
	}

    public struct RaycastInfo
    {
        public Vector3 ContactNormalWS;
        public Vector3 ContactPointWS;
        public Object GroundObject;
        public Vector3 HardPointWS;
        public bool IsInContact;
        public float SuspensionLength;
        public Vector3 WheelAxleWS;
        public Vector3 WheelDirectionWS;
    }

    public class WheelInfo
    {
        public WheelInfo(WheelInfoConstructionInfo ci)
        {
            SuspensionRestLength1 = ci.SuspensionRestLength;
            MaxSuspensionTravelCm = ci.MaxSuspensionTravelCm;

            WheelsRadius = ci.WheelRadius;
            SuspensionStiffness = ci.SuspensionStiffness;
            WheelsDampingCompression = ci.WheelsDampingCompression;
            WheelsDampingRelaxation = ci.WheelsDampingRelaxation;
            ChassisConnectionPointCS = ci.ChassisConnectionCS;
            WheelDirectionCS = ci.WheelDirectionCS;
            WheelAxleCS = ci.WheelAxleCS;
            FrictionSlip = ci.FrictionSlip;
            Steering = 0;
            EngineForce = 0;
            Rotation = 0;
            DeltaRotation = 0;
            Brake = 0;
            RollInfluence = 0.1f;
            IsFrontWheel = ci.IsFrontWheel;
            MaxSuspensionForce = ci.MaxSuspensionForce;

            //ClientInfo = IntPtr.Zero;
            //ClippedInvContactDotSuspension = 0;
            WorldTransform = Matrix.Identity;
            //WheelsSuspensionForce = 0;
            //SuspensionRelativeVelocity = 0;
            //SkidInfo = 0;
            RaycastInfo = new RaycastInfo();
        }

        public void UpdateWheel(RigidBody chassis, RaycastInfo raycastInfo)
        {
            if (raycastInfo.IsInContact)
            {
                float project = Vector3.Dot(raycastInfo.ContactNormalWS, raycastInfo.WheelDirectionWS);
                Vector3 chassis_velocity_at_contactPoint;
                Vector3 relpos = raycastInfo.ContactPointWS - chassis.CenterOfMassPosition;
                chassis_velocity_at_contactPoint = chassis.GetVelocityInLocalPoint(relpos);
                float projVel = Vector3.Dot(raycastInfo.ContactNormalWS, chassis_velocity_at_contactPoint);
                if (project >= -0.1f)
                {
                    SuspensionRelativeVelocity = 0;
                    ClippedInvContactDotSuspension = 1.0f / 0.1f;
                }
                else
                {
                    float inv = -1.0f / project;
                    SuspensionRelativeVelocity = projVel * inv;
                    ClippedInvContactDotSuspension = inv;
                }

            }

            else    // Not in contact : position wheel in a nice (rest length) position
            {
                RaycastInfo.SuspensionLength = SuspensionRestLength;
                SuspensionRelativeVelocity = 0;
                RaycastInfo.ContactNormalWS = -raycastInfo.WheelDirectionWS;
                ClippedInvContactDotSuspension = 1.0f;
            }
        }

        public float SuspensionRestLength
        {
            get { return SuspensionRestLength1; }
        }

        public bool IsFrontWheel;
        public float Brake;
        public Vector3 ChassisConnectionPointCS;
        public IntPtr ClientInfo;
        public float ClippedInvContactDotSuspension;
        public float DeltaRotation;
        public float EngineForce;
        public float FrictionSlip;
        public float MaxSuspensionForce;
        public float MaxSuspensionTravelCm;
        public RaycastInfo RaycastInfo;
        public float RollInfluence;
        public float Rotation;
        public float SkidInfo;
        public float Steering;
        public float SuspensionRelativeVelocity;
        public float SuspensionRestLength1;
        public float SuspensionStiffness;
        public Vector3 WheelAxleCS;
        public Vector3 WheelDirectionCS;
        public float WheelsDampingCompression;
        public float WheelsDampingRelaxation;
        public float WheelsRadius;
        public float WheelsSuspensionForce;
        public Matrix WorldTransform;
    }
}
