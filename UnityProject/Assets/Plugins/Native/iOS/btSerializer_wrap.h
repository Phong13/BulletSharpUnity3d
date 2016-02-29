#include "main.h"

#ifndef BT_SERIALIZER_H
#define pSerializer_Allocate void*
#define pSerializer_FinalizeChunk void*
#define pSerializer_FindNameForPointer void*
#define pSerializer_FindPointer void*
#define pSerializer_FinishSerialization void*
#define pSerializer_GetBufferPointer void*
#define pSerializer_GetChunk void*
#define pSerializer_GetCurrentBufferSize void*
#define pSerializer_GetNumChunks void*
#define pSerializer_GetSerializationFlags void*
#define pSerializer_GetUniquePointer void*
#define pSerializer_RegisterNameForPointer void*
#define pSerializer_SerializeName void*
#define pSerializer_SetSerializationFlags void*
#define pSerializer_StartSerialization void*
#define btSerializerWrapper void
#else
typedef btChunk* (*pSerializer_Allocate)(size_t size, int numElements);
typedef void (*pSerializer_FinalizeChunk)(btChunk* chunk, const char* structType, int chunkCode, void* oldPtr);
typedef const char* (*pSerializer_FindNameForPointer)(const void* ptr);
typedef void* (*pSerializer_FindPointer)(void* oldPtr);
typedef void (*pSerializer_FinishSerialization)();
typedef const unsigned char* (*pSerializer_GetBufferPointer)();
typedef btChunk* (*pSerializer_GetChunk)(int chunkIndex);
typedef int (*pSerializer_GetCurrentBufferSize)();
typedef int (*pSerializer_GetNumChunks)();
typedef int (*pSerializer_GetSerializationFlags)();
typedef void* (*pSerializer_GetUniquePointer)(void* oldPtr);
typedef void (*pSerializer_RegisterNameForPointer)(const void* ptr, const char* name);
typedef void (*pSerializer_SerializeName)(const char* ptr);
typedef void (*pSerializer_SetSerializationFlags)(int flags);
typedef void (*pSerializer_StartSerialization)();

class btSerializerWrapper : public btSerializer
{
private:
	pSerializer_Allocate _allocateCallback;
	pSerializer_FinalizeChunk _finalizeChunkCallback;
	pSerializer_FindNameForPointer _findNameForPointerCallback;
	pSerializer_FindPointer _findPointerCallback;
	pSerializer_FinishSerialization _finishSerializationCallback;
	pSerializer_GetBufferPointer _getBufferPointerCallback;
	pSerializer_GetChunk _getChunkCallback;
	pSerializer_GetCurrentBufferSize _getCurrentBufferSizeCallback;
	pSerializer_GetNumChunks _getNumChunksCallback;
	pSerializer_GetSerializationFlags _getSerializationFlagsCallback;
	pSerializer_GetUniquePointer _getUniquePointerCallback;
	pSerializer_RegisterNameForPointer _registerNameForPointerCallback;
	pSerializer_SerializeName _serializeNameCallback;
	pSerializer_SetSerializationFlags _setSerializationFlagsCallback;
	pSerializer_StartSerialization _startSerializationCallback;

public:
	btSerializerWrapper(pSerializer_Allocate allocateCallback, pSerializer_FinalizeChunk finalizeChunkCallback, pSerializer_FindNameForPointer findNameForPointerCallback, pSerializer_FindPointer findPointerCallback, pSerializer_FinishSerialization finishSerializationCallback, pSerializer_GetBufferPointer getBufferPointerCallback, pSerializer_GetChunk getChunkCallback, pSerializer_GetCurrentBufferSize getCurrentBufferSizeCallback, pSerializer_GetNumChunks getNumChunksCallback, pSerializer_GetSerializationFlags getSerializationFlagsCallback, pSerializer_GetUniquePointer getUniquePointerCallback, pSerializer_RegisterNameForPointer registerNameForPointerCallback, pSerializer_SerializeName serializeNameCallback, pSerializer_SetSerializationFlags setSerializationFlagsCallback, pSerializer_StartSerialization startSerializationCallback);

	virtual btChunk* allocate(size_t size, int numElements);
	virtual void finalizeChunk(btChunk* chunk, const char* structType, int chunkCode, void* oldPtr);
	virtual const char* findNameForPointer(const void* ptr) const;
	virtual void* findPointer(void* oldPtr);
	virtual void finishSerialization();
	virtual const unsigned char* getBufferPointer() const;
	virtual btChunk* getChunk(int chunkIndex) const;
	virtual int getCurrentBufferSize() const;
	virtual int getNumChunks() const;
	virtual int getSerializationFlags() const;
	virtual void* getUniquePointer(void* oldPtr);
	virtual void registerNameForPointer(const void* ptr, const char* name);
	virtual void serializeName(const char* ptr);
	virtual void setSerializationFlags(int flags);
	virtual void startSerialization();
};
#endif

extern "C"
{
	EXPORT btChunk* btChunk_new();
	EXPORT int btChunk_getChunkCode(btChunk* obj);
	EXPORT int btChunk_getDna_nr(btChunk* obj);
	EXPORT int btChunk_getLength(btChunk* obj);
	EXPORT int btChunk_getNumber(btChunk* obj);
	EXPORT void* btChunk_getOldPtr(btChunk* obj);
	EXPORT void btChunk_setChunkCode(btChunk* obj, int value);
	EXPORT void btChunk_setDna_nr(btChunk* obj, int value);
	EXPORT void btChunk_setLength(btChunk* obj, int value);
	EXPORT void btChunk_setNumber(btChunk* obj, int value);
	EXPORT void btChunk_setOldPtr(btChunk* obj, void* value);
	EXPORT void btChunk_delete(btChunk* obj);

	EXPORT btSerializerWrapper* btSerializerWrapper_new(pSerializer_Allocate allocateCallback,
		pSerializer_FinalizeChunk finalizeChunkCallback, pSerializer_FindNameForPointer findNameForPointerCallback,
		pSerializer_FindPointer findPointerCallback, pSerializer_FinishSerialization finishSerializationCallback,
		pSerializer_GetBufferPointer getBufferPointerCallback, pSerializer_GetChunk getChunkCallback,
		pSerializer_GetCurrentBufferSize getCurrentBufferSizeCallback, pSerializer_GetNumChunks getNumChunksCallback,
		pSerializer_GetSerializationFlags getSerializationFlagsCallback, pSerializer_GetUniquePointer getUniquePointerCallback,
		pSerializer_RegisterNameForPointer registerNameForPointerCallback, pSerializer_SerializeName serializeNameCallback,
		pSerializer_SetSerializationFlags setSerializationFlagsCallback, pSerializer_StartSerialization startSerializationCallback);
	EXPORT void btSerializer_delete(btSerializer* obj);

	EXPORT btDefaultSerializer* btDefaultSerializer_new();
	EXPORT btDefaultSerializer* btDefaultSerializer_new2(int totalSize);
	EXPORT unsigned char* btDefaultSerializer_internalAlloc(btDefaultSerializer* obj, size_t size);
	EXPORT void btDefaultSerializer_writeHeader(btDefaultSerializer* obj, unsigned char* buffer);

	EXPORT char* getBulletDNAstr();
	EXPORT int getBulletDNAlen();
	EXPORT char* getBulletDNAstr64();
	EXPORT int getBulletDNAlen64();
}
