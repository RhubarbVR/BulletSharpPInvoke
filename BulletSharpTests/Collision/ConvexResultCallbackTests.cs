using Microsoft.VisualStudio.TestTools.UnitTesting;
using BulletSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace BulletSharp.Tests
{
    [TestClass()]
    public class ConvexResultCallbackTests
    {
        [TestMethod()]
        public void RayTest()
        {
            var _collisionConfiguration = new DefaultCollisionConfiguration();
            var _dispatcher = new CollisionDispatcher(_collisionConfiguration);
            var _broadphase = new DbvtBroadphase();
            var _solverPool = new ConstraintSolverPoolMultiThreaded(Math.Max(Environment.ProcessorCount - 1, 1));
            var _parallelSolver = new SequentialImpulseConstraintSolverMultiThreaded();
            var _physicsWorld = new DiscreteDynamicsWorldMultiThreaded(_dispatcher, _broadphase, _solverPool, _parallelSolver, _collisionConfiguration);
            var grav = new Vector3(0, -10, 0);
            _physicsWorld.SetGravity(ref grav);
            var to = Vector3.UnitY;
            var from = Vector3.Zero;
            var box = new BoxShape(0.25f);
            var localInertia = Vector3.Zero;
            var rbInfo = new RigidBodyConstructionInfo(0, null, box, localInertia);
            var con = new RigidBody(rbInfo);
            _physicsWorld.AddCollisionObject(con);
            var ray = new ClosestRayResultCallback(ref from, ref to);
            for (int i = 0; i < 10; i++)
            {
                _physicsWorld.RayTest(from, to, ray);
                _physicsWorld.StepSimulation(1);
            }
        }

    }
}