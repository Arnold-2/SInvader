using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    class Subject : DLink
    {
        public Subject.Name name;
        public Observer poObserverHead;

        public enum Name
        {
            LeftKey,
            RightKey,
            SpaceKey,
            Uninitialized
        }

        public Subject()
        {
            this.name = Name.Uninitialized;
        }

        // Subscribe game object to subject
        public void Subscribe(GameObject go)
        {
            // TODO: add null observer
            Observer obs = null;

            // adding derived observer based on name
            if (name == Subject.Name.LeftKey || name == Subject.Name.RightKey)
            {
                // for left key and right key event
                // create a move observer to listen
                obs = new MoveObserver();
                ((MoveObserver)obs).set(name, (MoveProxy)go.pProxy);
            } else if (name == Name.SpaceKey)
            {
                // for space key, create a shoot observer
                obs = new ShootObserver();
            }

            // Add new observer to the list
            this.AddObserver(obs);
        }

        // Add new observer to the list
        public void AddObserver(Observer _obs)
        {
            if (this.poObserverHead == null)
            {
                _obs.Clear();
                this.poObserverHead = _obs;
            }
            else // Add to the front of list
            {
                _obs.Clear();
                _obs.pNext = this.poObserverHead;
                this.poObserverHead.pPrev = _obs;
                this.poObserverHead = _obs;
            }
        }

        // Update Subject's observers
        // --Better way is to create derived class to override this method
        // --Here I'm making it working first
        public virtual void Update()
        {
            if (
                    (this.name == Name.LeftKey && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT)) 
                    || (this.name == Name.RightKey && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT))
                    || (this.name == Name.SpaceKey && Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE))
                )
            {
                // notify all observers
                this.Notify();
            }
        }

        // notify all observers
        public void Notify()
        {
            Observer current = this.poObserverHead;

            while (current != null)
            {
                current.Notify();

                current = (Observer)current.pNext;
            }
        }

        public override bool DerivedEqual(DLink node)
        {
            if (((Subject)node).name == this.name)
                return true;
            else
                return false;
        }

        public override void dbgDerivedPrint()
        {
            Debug.WriteLine("NodeType:  Subject");
            Debug.WriteLine("Node ID:   " + this.GetHashCode());
            Debug.WriteLine("Name:      " + this.name);
            Debug.WriteLine("--------------");
        }

        
    }
}
