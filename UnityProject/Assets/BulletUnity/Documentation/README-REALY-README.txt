GETTING STARTED WITH BULLET PHYSICS
===================================

SOURCES OF INFORMATION

The Bullet Physics Manual. Download it from the Bullet Physics project on github. It is short (can be read in 1 hour) and will set you up well for working with 
Bullet Physics.

The Bullet Physics Wiki has a lot of good information http://bulletphysics.org/mediawiki-1.5.8/index.php/Main_Page.

The Bullet Physics Forums http://www.bulletphysics.org/Bullet/phpBB3/

The Bullet Physics Examples (ported to C#, then ported to Unity) located in the BulletUnity/BulletSharpExamples folder. At the time of writing not all of these
have been ported. More are available in the https://github.com/Phong13/BulletSharpPInvoke project.

DON'T BE AFRAID TO LOOK AT THE BULLET PHYSICS SOURCE CODE. I know it sounds intimidating, but it is much easier than you think. If you are not sure what an API
 call or class does or what a member variable is for, then search for it in the bullet sourcecode. Even if you are not a C++ programmer you can probably deduce
 what it does. I recently spent a few hours on google trying to find good information explaining the difference between btGhostObject and btPairCachingGhostObject.
Eventually I opened the btGhostObject.cpp source file and had a look. The entire sourcecode for both classes is only 170 lines. In about 10 minutes I had a complete
understanding how both classes worked. Much better than an online tutorial.

It is possible to debug from Unity into the Bullet Physics library with Visual Studio (probably possible with other IDEs but I havn't tried). To do this you need
to clone the https://github.com/Phong13/BulletSharpPInvoke project. Build a debug, x64 version of libbulletc for windows. Copy the .dll and .pdb file to Unity project. 
Then launch Unity from the Visual Studio as described here https://msdn.microsoft.com/en-us/library/605a12zt.aspx.

IMPORTANT DIFFERENCES WITH UNITY'S PHYSICS

Don't try to move the rigid bodies by writing to myRigidBody.transform.position or .rotation. Bullet Physics is not as tightly integrated with Unity as PhysX.
Consider the transform to be completely under the control of Bullet Physics and translate/rotate your rigidBodies using the Bullet Physics API calls.

Be careful of localScale. It is almost completely ignored by Bullet Physics. There are only a few CollisionShapes that can be scaled in the Bullet API (at the time
of writing these have not been implemented in Unity). Modify the shape of the CollisionShape and leave localScale at 1,1,1.

  