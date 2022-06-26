#include <BulletCollision/CollisionShapes/btTriangleCallback.h>

#include "conversion.h"
#include "btTriangleCallback_wrap.h"

btInternalTriangleIndexCallbackWrapper::btInternalTriangleIndexCallbackWrapper(p_btInternalTriangleIndexCallback_internalProcessTriangleIndex internalProcessTriangleIndexCallback, rhu_uint64_t target)
{
	_internalProcessTriangleIndexCallback = internalProcessTriangleIndexCallback;
	_target = target;
}

void btInternalTriangleIndexCallbackWrapper::internalProcessTriangleIndex(btVector3* triangle,
	int partId, int triangleIndex)
{
	_internalProcessTriangleIndexCallback(_target,triangle, partId, triangleIndex);
}


btTriangleCallbackWrapper::btTriangleCallbackWrapper(p_btTriangleCallback_processTriangle processTriangleCallback, rhu_uint64_t target)
{
	_processTriangleCallback = processTriangleCallback;
	_target = target;
}

void btTriangleCallbackWrapper::processTriangle(btVector3* triangle, int partId,
	int triangleIndex)
{
	_processTriangleCallback(_target,triangle, partId, triangleIndex);
}


btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(p_btTriangleCallback_processTriangle processTriangleCallback, rhu_uint64_t target)
{
	return new btTriangleCallbackWrapper(processTriangleCallback, target);
}

void btTriangleCallback_delete(btTriangleCallback* obj)
{
	delete obj;
}


btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(
	p_btInternalTriangleIndexCallback_internalProcessTriangleIndex internalProcessTriangleIndexCallback, rhu_uint64_t target)
{
	return new btInternalTriangleIndexCallbackWrapper(internalProcessTriangleIndexCallback, target);
}

void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj)
{
	delete obj;
}
