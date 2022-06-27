#include "main.h"

#ifndef BT_TRIANGLE_CALLBACK_H
#define p_btInternalTriangleIndexCallback_internalProcessTriangleIndex void*
#define p_btTriangleCallback_processTriangle void*
#define btInternalTriangleIndexCallbackWrapper void
#define btTriangleCallbackWrapper void
#else

typedef void (*p_btInternalTriangleIndexCallback_internalProcessTriangleIndex)(rhu_uint64_t target, btVector3* triangle,
	int partId, int triangleIndex);

class btInternalTriangleIndexCallbackWrapper : public btInternalTriangleIndexCallback
{
private:
	p_btInternalTriangleIndexCallback_internalProcessTriangleIndex _internalProcessTriangleIndexCallback;
	rhu_uint64_t _target;

public:
	btInternalTriangleIndexCallbackWrapper(p_btInternalTriangleIndexCallback_internalProcessTriangleIndex internalProcessTriangleIndexCallback, rhu_uint64_t target);

	virtual void internalProcessTriangleIndex(btVector3* triangle, int partId, int triangleIndex);
};

typedef void (*p_btTriangleCallback_processTriangle)(rhu_uint64_t target,btVector3* triangle, int partId,
	int triangleIndex);

class btTriangleCallbackWrapper : public btTriangleCallback
{
private:
	p_btTriangleCallback_processTriangle _processTriangleCallback;
	rhu_uint64_t _target;

public:
	btTriangleCallbackWrapper(p_btTriangleCallback_processTriangle processTriangleCallback, rhu_uint64_t target);

	virtual void processTriangle(btVector3* triangle, int partId, int triangleIndex);
};
#endif

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btTriangleCallbackWrapper* btTriangleCallbackWrapper_new(p_btTriangleCallback_processTriangle processTriangleCallback, rhu_uint64_t target);

	EXPORT void btTriangleCallback_delete(btTriangleCallback* obj);

	EXPORT btInternalTriangleIndexCallbackWrapper* btInternalTriangleIndexCallbackWrapper_new(
		p_btInternalTriangleIndexCallback_internalProcessTriangleIndex internalProcessTriangleIndexCallback, rhu_uint64_t tar);

	EXPORT void btInternalTriangleIndexCallback_delete(btInternalTriangleIndexCallback* obj);
#ifdef __cplusplus
}
#endif
