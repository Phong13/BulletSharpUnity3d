GETTING STARTED WITH BULLET PHYSICS
===================================
INSTALLATION

Bullet Physics For Unity uses unsafe code. There must be a file "smcs.rsp" in the root asset folder that contains:

-unsafe

Sometimes this file is not included

===================================

BULLET UNITY CONTAINS TWO API'S

1) BulletSharp - Located in Plugins/BulletSharp is a low level set of C# wrappers for the native bullet libraries. These wrappers are not integrated with Unity in
any way. Simulations can be run that are not synchronized with Unity's game loop. The demos in BulletUnity/Examples/Scenes/BulletSharpDemos use this API.

2) BulletUnity - Located in BulletUnity/Scripts is a set of Unity Components similar to the PhysX components. These components use the lower level BulletSharp API.

======================
SOURCES OF INFORMATION

The BULLET PHYSICS MANUAL. Download it from the Bullet Physics project on github. It is short (can be read in 1 hour) and will set you up well for working with 
Bullet Physics.

The Bullet Physics Wiki has a lot of good information http://bulletphysics.org/mediawiki-1.5.8/index.php/Main_Page.

The Bullet Physics Forums http://www.bulletphysics.org/Bullet/phpBB3/

The Bullet Physics Examples (ported to C#, then ported to Unity) located in the BulletUnity/Examples/Scenes/BulletSharpDemos folder. More are available in the https://github.com/Phong13/BulletSharpPInvoke project.

DON'T BE AFRAID TO LOOK AT THE BULLET PHYSICS SOURCE CODE. I know it sounds intimidating, but it is much easier than you think. If you are not sure what an API
 call or class does or what a member variable is for, then search for it in the bullet sourcecode. Even if you are not a C++ programmer you can probably deduce
 what it does. I recently spent a few hours on google trying to find good information explaining the difference between btGhostObject and btPairCachingGhostObject.
Eventually I opened the btGhostObject.cpp source file and had a look. The entire sourcecode for both classes is only 170 lines. In about 10 minutes I had a complete
understanding how both classes worked. Much better than an online tutorial.

It is possible to debug from Unity into the Bullet Physics library native code with Visual Studio (probably possible with other IDEs but I havn't tried). To do this you need
to clone the https://github.com/Phong13/BulletSharpPInvoke project. Build a debug, x64 version of libbulletc for windows. Copy the .dll and .pdb file to Unity project (Plugins/Native/x64. 
Then launch Unity from the Visual Studio as described here https://msdn.microsoft.com/en-us/library/605a12zt.aspx.

================================================
IMPORTANT DIFFERENCES WITH UNITY'S PHYSX PHYSICS

Don't try to move the rigid bodies by writing to myRigidBody.transform.position or .rotation. Bullet Physics is not as tightly integrated with Unity as PhysX.
Consider the transform to be completely under the control of Bullet Physics (for non-kinematic RigidBodies) and translate/rotate your rigidBodies using the Bullet Physics API calls.

Be careful of localScale. It is almost completely ignored by Bullet Physics. There are only a few CollisionShapes that can be scaled in the Bullet API (at the time
of writing these have not been implemented in Unity). Modify the shape of the CollisionShape and leave localScale at 1,1,1. You can add your MeshRender as a child of
the CollisionShape and scale that.

Don't try to nest Rigid Bodies. Bullet Unity has no control over the order that bullet updates the transforms of objects each simulation step. If the child RigidBodies get
updated before the parent RigidBodies then the child will jitter terribly.

	WRONG
		RigidBodyGameObjectA
			-RigidBodyGameObjectB
				-RigidBodyGameObjectC

	CORRECT
		RigidBodyGameObjectA
		RigidBodyGameObjectB
		RigidBodyGameObjectC

======================================
FEEL FREE TO CONTRIBUTE TO THE PROJECT

Bullet Unity is an open source project in GitHub. Please feel free to clone the github repository and contribute:

	https://github.com/Phong13/BulletUnity
	https://github.com/Phong13/BulletSharpPInvoke



  