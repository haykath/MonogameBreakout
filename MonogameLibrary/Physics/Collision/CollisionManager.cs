using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Physics
{   

    public class CollisionManager
    {
        protected struct CollisionPair
        {
            public ICollideable A;
            public ICollideable B;

            public CollisionPair(ICollideable A, ICollideable B)
            {
                this.A = A;
                this.B = B;
            }

            public static bool operator ==(CollisionPair pairA, CollisionPair pairB) {
                return (pairA.A == pairB.A && pairA.B == pairB.B) || (pairA.B == pairB.A && pairA.A == pairB.B);
            }

            public static bool operator !=(CollisionPair pairA, CollisionPair pairB)
            {
                return !((pairA.A == pairB.A && pairA.B == pairB.B) || (pairA.B == pairB.A && pairA.A == pairB.B));
            }
        }

        private static CollisionManager instance;
        
        public static CollisionManager GetManager
        {
            get
            {
                if (instance == null)
                    instance = new CollisionManager();
                return instance;
            }
        }

        protected List<ICollideable> collideableObjects;
        protected List<CollisionPair> currentCollisions; 

        protected CollisionManager()
        {
            collideableObjects = new List<ICollideable>();
            currentCollisions = new List<CollisionPair>();
        }

        public static void AddObject(ICollideable obj)
        {
            CollisionManager.GetManager.collideableObjects.Add(obj);
        }

        public void TestAll()
        {
            for (int i = 0; i < collideableObjects.Count; i++)
            {
                for(int j = i + 1; j < collideableObjects.Count; j++)
                {
                    Rectangle intersection = CollisionTest(collideableObjects[i], collideableObjects[j]);
                    CollisionPair pair = new CollisionPair(collideableObjects[i], collideableObjects[j]);
                    CollisionPair lastCollision = currentCollisions.Find(x => x == pair);

                    if (intersection != Rectangle.Empty)
                    {
                        CollisionInfo infoA = new CollisionInfo(collideableObjects[j], intersection, intersection.Center.ToVector2());
                        CollisionInfo infoB = new CollisionInfo(collideableObjects[i], intersection, intersection.Center.ToVector2());
                        collideableObjects[i].OnCollision(infoA);
                        collideableObjects[j].OnCollision(infoB);

                        if (lastCollision.A == null)
                        {
                            currentCollisions.Add(pair);
                            collideableObjects[i].OnCollisionEnter(infoA);
                            collideableObjects[j].OnCollisionEnter(infoB);
                        }
                    }
                    else
                    {
                        if (lastCollision.A != null)
                        {
                            currentCollisions.Remove(lastCollision);
                            collideableObjects[i].OnCollisionExit(collideableObjects[j]);
                            collideableObjects[j].OnCollisionExit(collideableObjects[i]);
                        }
                    }
                }
            }
        }

        public Rectangle CollisionTest(ICollideable A, ICollideable B){
            return BroadCheck(A, B);
        }

        protected Rectangle BroadCheck(ICollideable A, ICollideable B)
        {
            int xMin = Math.Max(A.Bounds.Left, B.Bounds.Left);
            int xMax = Math.Min(A.Bounds.Right, B.Bounds.Right);
            int yMin = Math.Max(A.Bounds.Top, B.Bounds.Top);
            int yMax = Math.Min(A.Bounds.Bottom, B.Bounds.Bottom);

            if (xMin <= xMax && yMin <= yMax)
            {
                return new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
            }
            return Rectangle.Empty;
        }
    }
}
