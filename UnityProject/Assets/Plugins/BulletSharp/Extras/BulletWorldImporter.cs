using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace BulletSharp
{
	public class BulletWorldImporter : WorldImporter
	{
		public BulletWorldImporter(DynamicsWorld world)
			: base(world)
		{
		}

		public BulletWorldImporter()
			: base(null)
		{
		}
        
		public bool ConvertAllObjects(BulletFile file)
		{
            _shapeMap.Clear();
            _bodyMap.Clear();

            foreach (byte[] bvhData in file.Bvhs)
            {
                OptimizedBvh bvh = CreateOptimizedBvh();

                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    // QuantizedBvhData is parsed in C++, so we need to actually fix pointers
                    GCHandle bvhDataHandle = GCHandle.Alloc(bvhData, GCHandleType.Pinned);
                    IntPtr bvhDataPinnedPtr = bvhDataHandle.AddrOfPinnedObject();

                    IntPtr contiguousNodesHandlePtr = IntPtr.Zero;
                    IntPtr quantizedContiguousNodesHandlePtr = IntPtr.Zero;
                    IntPtr subTreeInfoHandlePtr = IntPtr.Zero;

                    using (MemoryStream stream = new MemoryStream(bvhData))
                    {
                        using (BulletReader reader = new BulletReader(stream))
                        {
                            long contiguousNodesPtr = reader.ReadPtr(QuantizedBvhFloatData.Offset("ContiguousNodesPtr"));
                            long quantizedContiguousNodesPtr = reader.ReadPtr(QuantizedBvhFloatData.Offset("QuantizedContiguousNodesPtr"));
                            long subTreeInfoPtr = reader.ReadPtr(QuantizedBvhFloatData.Offset("SubTreeInfoPtr"));

                            using (BulletWriter writer = new BulletWriter(stream))
                            {
                                if (contiguousNodesPtr != 0)
                                {
                                    GCHandle contiguousNodesHandle = GCHandle.Alloc(file.LibPointers[contiguousNodesPtr], GCHandleType.Pinned);
                                    contiguousNodesHandlePtr = GCHandle.ToIntPtr(contiguousNodesHandle);
                                    stream.Position = QuantizedBvhFloatData.Offset("ContiguousNodesPtr");
                                    writer.Write(contiguousNodesHandle.AddrOfPinnedObject());
                                }
                                if (quantizedContiguousNodesPtr != 0)
                                {
                                    GCHandle quantizedContiguousNodesHandle = GCHandle.Alloc(file.LibPointers[quantizedContiguousNodesPtr], GCHandleType.Pinned);
                                    quantizedContiguousNodesHandlePtr = GCHandle.ToIntPtr(quantizedContiguousNodesHandle);
                                    stream.Position = QuantizedBvhFloatData.Offset("QuantizedContiguousNodesPtr");
                                    writer.Write(quantizedContiguousNodesHandle.AddrOfPinnedObject());
                                }
                                if (subTreeInfoPtr != 0)
                                {
                                    GCHandle subTreeInfoHandle = GCHandle.Alloc(file.LibPointers[subTreeInfoPtr], GCHandleType.Pinned);
                                    subTreeInfoHandlePtr = GCHandle.ToIntPtr(subTreeInfoHandle);
                                    stream.Position = QuantizedBvhFloatData.Offset("SubTreeInfoPtr");
                                    writer.Write(subTreeInfoHandle.AddrOfPinnedObject());
                                }
                            }
                        }
                    }

                    bvh.DeSerializeFloat(bvhDataPinnedPtr);
                    bvhDataHandle.Free();

                    if (contiguousNodesHandlePtr != IntPtr.Zero)
                    {
                        GCHandle.FromIntPtr(contiguousNodesHandlePtr).Free();
                    }
                    if (quantizedContiguousNodesHandlePtr != IntPtr.Zero)
                    {
                        GCHandle.FromIntPtr(quantizedContiguousNodesHandlePtr).Free();
                    }
                    if (subTreeInfoHandlePtr != IntPtr.Zero)
                    {
                        GCHandle.FromIntPtr(subTreeInfoHandlePtr).Free();
                    }
                }

                foreach (KeyValuePair<long, byte[]> lib in file.LibPointers)
                {
                    if (lib.Value == bvhData)
                    {
                        _bvhMap.Add(lib.Key, bvh);
                        break;
                    }
                }
            }

            foreach (byte[] shapeData in file.CollisionShapes)
            {
                CollisionShape shape = ConvertCollisionShape(shapeData, file.LibPointers);
                if (shape != null)
                {
                    foreach (KeyValuePair<long, byte[]> lib in file.LibPointers)
                    {
                        if (lib.Value == shapeData)
                        {
                            _shapeMap.Add(lib.Key, shape);
                            break;
                        }
                    }

                    using (MemoryStream stream = new MemoryStream(shapeData, false))
                    {
                        using (BulletReader reader = new BulletReader(stream))
                        {
                            long namePtr = reader.ReadPtr(CollisionShapeFloatData.Offset("Name"));
                            if (namePtr != 0)
                            {
                                byte[] nameData = file.LibPointers[namePtr];
                                int length = Array.IndexOf(nameData, (byte)0);
                                string name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                                _objectNameMap.Add(shape, name);
                                _nameShapeMap.Add(name, shape);
                            }
                        }
                    }
                }
            }

            foreach (byte[] solverInfoData in file.DynamicsWorldInfo)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    //throw new NotImplementedException();
                }
                else
                {
                    //throw new NotImplementedException();
                }
            }

            foreach (byte[] bodyData in file.RigidBodies)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ConvertRigidBodyFloat(bodyData, file.LibPointers);
                }
            }

            foreach (byte[] colObjData in file.CollisionObjects)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    using (MemoryStream colObjStream = new MemoryStream(colObjData, false))
                    {
                        using (BulletReader colObjReader = new BulletReader(colObjStream))
                        {
                            long shapePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("CollisionShape"));
                            CollisionShape shape = _shapeMap[shapePtr];
                            Math.Matrix startTransform = colObjReader.ReadMatrix(CollisionObjectFloatData.Offset("WorldTransform"));
                            long namePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("Name"));
                            string name = null;
                            if (namePtr != 0)
                            {
                                byte[] nameData = file.FindLibPointer(namePtr);
                                int length = Array.IndexOf(nameData, (byte)0);
                                name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                            }
                            CollisionObject colObj = CreateCollisionObject(ref startTransform, shape, name);
                            _bodyMap.Add(colObjData, colObj);
                        }
                    }
                }
            }

            foreach (byte[] constraintData in file.Constraints)
            {
                MemoryStream stream = new MemoryStream(constraintData, false);
                using (BulletReader reader = new BulletReader(stream))
                {
                    long collisionObjectAPtr = reader.ReadPtr(TypedConstraintFloatData.Offset("RigidBodyA"));
                    long collisionObjectBPtr = reader.ReadPtr(TypedConstraintFloatData.Offset("RigidBodyB"));

                    RigidBody a = null, b = null;

                    if (collisionObjectAPtr != 0)
                    {
                        if (!file.LibPointers.ContainsKey(collisionObjectAPtr))
                        {
                            a = TypedConstraint.GetFixedBody();
                        }
                        else
                        {
                            byte[] coData = file.LibPointers[collisionObjectAPtr];
                            a = RigidBody.Upcast(_bodyMap[coData]);
                            if (a == null)
                            {
                                a = TypedConstraint.GetFixedBody();
                            }
                        }
                    }

                    if (collisionObjectBPtr != 0)
                    {
                        if (!file.LibPointers.ContainsKey(collisionObjectBPtr))
                        {
                            b = TypedConstraint.GetFixedBody();
                        }
                        else
                        {
                            byte[] coData = file.LibPointers[collisionObjectBPtr];
                            b = RigidBody.Upcast(_bodyMap[coData]);
                            if (b == null)
                            {
                                b = TypedConstraint.GetFixedBody();
                            }
                        }
                    }

                    if (a == null && b == null)
                    {
                        stream.Dispose();
                        continue;
                    }

                    if ((file.Flags & FileFlags.DoublePrecision) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        ConvertConstraintFloat(a, b, constraintData, file.Version, file.LibPointers);
                    }
                }
                stream.Dispose();
            }

            return true;
		}

        public bool LoadFile(string fileName, string preSwapFilenameOut)
		{
			BulletFile bulletFile = new BulletFile(fileName);
            bool result = LoadFileFromMemory(bulletFile);

            //now you could save the file in 'native' format using
            //bulletFile.WriteFile("native.bullet");
            if (result)
            {
                    if (preSwapFilenameOut != null)
                    {
                        bulletFile.PreSwap();
                        //bulletFile.WriteFile(preSwapFilenameOut);
                    }
                
            }

            return result;
		}

        public bool LoadFile(string fileName)
        {
            return LoadFile(fileName, null);
        }
        
		public bool LoadFileFromMemory(byte[] memoryBuffer, int len)
		{
            BulletFile bulletFile = new BulletFile(memoryBuffer, len);
            return LoadFileFromMemory(bulletFile);
		}
        
        public bool LoadFileFromMemory(BulletFile bulletFile)
		{
            if ((bulletFile.Flags & FileFlags.OK) != FileFlags.OK)
            {
                return false;
            }

            bulletFile.Parse(_verboseMode);

            if ((_verboseMode & FileVerboseMode.DumpChunks) == FileVerboseMode.DumpChunks)
            {
                //bulletFile.DumpChunks(bulletFile->FileDna);
            }

            return ConvertAllObjects(bulletFile);
		}
	}
}
