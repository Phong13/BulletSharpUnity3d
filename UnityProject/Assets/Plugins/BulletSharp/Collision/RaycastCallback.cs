using System;
using BulletSharp.Math;

namespace BulletSharp
{
	public abstract class TriangleRaycastCallback : TriangleCallback
	{
		[Flags]
		public enum EFlags
		{
			None = 0,
            FilterBackfaces = 1,
            KeepUnflippedNormal = 2,
            UseSubSimplexConvexCastRaytest = 4,
            UseGjkConvexCastRaytest = 8,
            Terminator = -1
		}

        public TriangleRaycastCallback(ref Vector3 from, ref Vector3 to, EFlags flags)
        {
            HitFraction = 1.0f;
        }

        public TriangleRaycastCallback(ref Vector3 from, ref Vector3 to)
            : this(ref from, ref to, EFlags.None)
        {
        }

        public override void ProcessTriangle(ref Vector3 point0, ref Vector3 point1, ref Vector3 point2, int partId, int triangleIndex)
        {
            Vector3 v10 = point1 - point0;
            Vector3 v20 = point2 - point0;

            Vector3 triangleNormal = v10.Cross(v20);

            float dist;
            point0.Dot(ref triangleNormal, out dist);
            float distA = triangleNormal.Dot(From) - dist;
            float distB = triangleNormal.Dot(To) - dist;

            if (distA * distB >= 0.0f)
            {
                return; // same sign
            }

            if (((Flags & EFlags.FilterBackfaces) != 0) && (distA <= 0.0f))
            {
                // Backface, skip check
                return;
            }


            float proj_length = distA - distB;
            float distance = (distA) / (proj_length);
            // Now we have the intersection point on the plane, we'll see if it's inside the triangle
            // Add an epsilon as a tolerance for the raycast,
            // in case the ray hits exacly on the edge of the triangle.
            // It must be scaled for the triangle size.

            if (distance < HitFraction)
            {
                float edgeTolerance = triangleNormal.LengthSquared;
                edgeTolerance *= -0.0001f;
                Vector3 point = Vector3.Lerp(From, To, distance);
                {
                    Vector3 v0p; v0p = point0 - point;
                    Vector3 v1p; v1p = point1 - point;
                    Vector3 cp0; cp0 = v0p.Cross(v1p);

                    float dot;
                    cp0.Dot(ref triangleNormal, out dot);
                    if (dot >= edgeTolerance)
                    {
                        Vector3 v2p; v2p = point2 - point;
                        Vector3 cp1;
                        cp1 = v1p.Cross(v2p);
                        cp1.Dot(ref triangleNormal, out dot);
                        if (dot >= edgeTolerance)
                        {
                            Vector3 cp2;
                            cp2 = v2p.Cross(v0p);

                            cp2.Dot(ref triangleNormal, out dot);
                            if (dot >= edgeTolerance)
                            {
                                //@BP Mod
                                // Triangle normal isn't normalized
                                triangleNormal.Normalize();

                                //@BP Mod - Allow for unflipped normal when raycasting against backfaces
                                if (((Flags & EFlags.KeepUnflippedNormal) == 0) && (distA <= 0.0f))
                                {
                                    triangleNormal = -triangleNormal;
                                }
                                HitFraction = ReportHit(ref triangleNormal, distance, partId, triangleIndex);
                            }
                        }
                    }
                }
            }
        }

        public abstract float ReportHit(ref Vector3 hitNormalLocal, float hitFraction, int partId, int triangleIndex);

        public EFlags Flags { get; set; }
        public Vector3 From { get; set; }
        public float HitFraction { get; set; }
        public Vector3 To { get; set; }
	}

	public abstract class TriangleConvexcastCallback : TriangleCallback
	{
        public TriangleConvexcastCallback(ConvexShape convexShape, ref Matrix convexShapeFrom, ref Matrix convexShapeTo, ref Matrix triangleToWorld, float triangleCollisionMargin)
        {
            ConvexShape = convexShape;
            ConvexShapeFrom = convexShapeFrom;
            ConvexShapeTo = convexShapeTo;
            TriangleToWorld = triangleToWorld;
            TriangleCollisionMargin = triangleCollisionMargin;

            AllowedPenetration = 0.0f;
            HitFraction = 1.0f;
        }

        public override void ProcessTriangle(ref Vector3 point0, ref Vector3 point1, ref Vector3 point2, int partId, int triangleIndex)
        {
            throw new NotImplementedException();
        }

        public abstract float ReportHit(ref Vector3 hitNormalLocal, ref Vector3 hitPointLocal, float hitFraction, int partId, int triangleIndex);

        public float AllowedPenetration { get; set; }
        public ConvexShape ConvexShape { get; set; }
        public Matrix ConvexShapeFrom { get; set; }
        public Matrix ConvexShapeTo { get; set; }
        public float HitFraction { get; set; }
        public float TriangleCollisionMargin { get; set; }
        public Matrix TriangleToWorld { get; set; }
	}
}
