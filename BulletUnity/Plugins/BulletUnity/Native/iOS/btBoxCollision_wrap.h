#include "main.h"

extern "C"
{
	EXPORT btAABB* btAABB_new();
	EXPORT btAABB* btAABB_new2(const btScalar* V1, const btScalar* V2, const btScalar* V3);
	EXPORT btAABB* btAABB_new3(const btScalar* V1, const btScalar* V2, const btScalar* V3, btScalar margin);
	EXPORT btAABB* btAABB_new4(const btAABB* other);
	EXPORT btAABB* btAABB_new5(const btAABB* other, btScalar margin);
	EXPORT void btAABB_appy_transform(btAABB* obj, const btScalar* trans);
	EXPORT void btAABB_appy_transform_trans_cache(btAABB* obj, const BT_BOX_BOX_TRANSFORM_CACHE* trans);
	EXPORT bool btAABB_collide_plane(btAABB* obj, const btScalar* plane);
	EXPORT bool btAABB_collide_ray(btAABB* obj, const btScalar* vorigin, const btScalar* vdir);
	EXPORT bool btAABB_collide_triangle_exact(btAABB* obj, const btScalar* p1, const btScalar* p2, const btScalar* p3, const btScalar* triangle_plane);
	EXPORT void btAABB_copy_with_margin(btAABB* obj, const btAABB* other, btScalar margin);
	EXPORT void btAABB_find_intersection(btAABB* obj, const btAABB* other, btAABB* intersection);
	EXPORT void btAABB_get_center_extend(btAABB* obj, btScalar* center, btScalar* extend);
	EXPORT void btAABB_getMax(btAABB* obj, btScalar* value);
	EXPORT void btAABB_getMin(btAABB* obj, btScalar* value);
	EXPORT bool btAABB_has_collision(btAABB* obj, const btAABB* other);
	EXPORT void btAABB_increment_margin(btAABB* obj, btScalar margin);
	EXPORT void btAABB_invalidate(btAABB* obj);
	EXPORT void btAABB_merge(btAABB* obj, const btAABB* box);
	EXPORT bool btAABB_overlapping_trans_cache(btAABB* obj, const btAABB* box, const BT_BOX_BOX_TRANSFORM_CACHE* transcache, bool fulltest);
	EXPORT bool btAABB_overlapping_trans_conservative(btAABB* obj, const btAABB* box, btScalar* trans1_to_0);
	EXPORT bool btAABB_overlapping_trans_conservative2(btAABB* obj, const btAABB* box, const BT_BOX_BOX_TRANSFORM_CACHE* trans1_to_0);
	EXPORT eBT_PLANE_INTERSECTION_TYPE btAABB_plane_classify(btAABB* obj, const btScalar* plane);
	EXPORT void btAABB_projection_interval(btAABB* obj, const btScalar* direction, btScalar* vmin, btScalar* vmax);
	EXPORT void btAABB_setMax(btAABB* obj, const btScalar* value);
	EXPORT void btAABB_setMin(btAABB* obj, const btScalar* value);
	EXPORT void btAABB_delete(btAABB* obj);
}
