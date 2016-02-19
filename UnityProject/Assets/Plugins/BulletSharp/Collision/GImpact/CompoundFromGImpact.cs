using BulletSharp.Math;

namespace BulletSharp
{
    class MyCallback : TriangleRaycastCallback
    {
        int _ignorePart;
        int _ignoreTriangleIndex;

        public MyCallback(ref Vector3 from, ref Vector3 to, int ignorePart, int ignoreTriangleIndex)
		    : base(ref from, ref to)
		{
            _ignorePart = ignorePart;
            _ignoreTriangleIndex = ignoreTriangleIndex;
		}

        public override float ReportHit(ref Vector3 hitNormalLocal, float hitFraction, int partId, int triangleIndex)
        {
            if (partId != _ignorePart || triangleIndex != _ignoreTriangleIndex)
            {
                if (hitFraction < HitFraction)
                    return hitFraction;
            }

            return HitFraction;
        }
    }

    class MyInternalTriangleIndexCallback : InternalTriangleIndexCallback
    {
        private CompoundShape _colShape;
        private float _depth;
        private GImpactMeshShape _meshShape;

        public MyInternalTriangleIndexCallback(CompoundShape colShape, GImpactMeshShape meshShape, float depth)
        {
            _colShape = colShape;
            _depth = depth;
            _meshShape = meshShape;
        }

        public override void InternalProcessTriangleIndex(ref Vector3 vertex0, ref Vector3 vertex1, ref Vector3 vertex2, int partId, int triangleIndex)
        {
            Vector3 scale = _meshShape.LocalScaling;
			Vector3 v0=vertex0*scale;
			Vector3 v1=vertex1*scale;
			Vector3 v2=vertex2*scale;
				
			Vector3 centroid = (v0+v1+v2)/3;
			Vector3 normal = (v1-v0).Cross(v2-v0);
			normal.Normalize();
			Vector3 rayFrom = centroid;
			Vector3 rayTo = centroid-normal*_depth;
				
			MyCallback cb = new MyCallback(ref rayFrom, ref rayTo, partId, triangleIndex);
				
			_meshShape.ProcessAllTrianglesRay(cb, ref rayFrom, ref rayTo);
			if (cb.HitFraction < 1)
			{
                rayTo = Vector3.Lerp(cb.From, cb.To, cb.HitFraction);
				//rayTo = cb.From;
				//gDebugDraw.drawLine(tr(centroid),tr(centroid+normal),btVector3(1,0,0));
			}

			BuSimplex1To4 tet = new BuSimplex1To4(v0,v1,v2,rayTo);
			_colShape.AddChildShape(Matrix.Identity, tet);
        }
    }

	public sealed class CompoundFromGImpact
	{
        private CompoundFromGImpact()
		{
		}

	    public static CompoundShape Create(GImpactMeshShape gImpactMesh, float depth)
	    {
            CompoundShape colShape = new CompoundShape();
            using (MyInternalTriangleIndexCallback cb = new MyInternalTriangleIndexCallback(colShape, gImpactMesh, depth))
            {
                Vector3 aabbMin, aabbMax;
                gImpactMesh.GetAabb(Matrix.Identity, out aabbMin, out aabbMax);
                gImpactMesh.MeshInterface.InternalProcessAllTriangles(cb, aabbMin, aabbMax);
            }
            return colShape;
	    }
	}
}
