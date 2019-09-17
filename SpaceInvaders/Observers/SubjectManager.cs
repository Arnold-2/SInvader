using System;

namespace SpaceInvaders
{
    class SubjectManager : Manager
    {
        private static SubjectManager pInstance;

        public SubjectManager() : base(3, 2)
        {

        }

        // Singleton
        public static SubjectManager getInstance()
        {
            if (pInstance == null)
            {
                pInstance = new SubjectManager();
            }

            return pInstance;
        }

        // Add one subject to the list
        public Subject AddSubject(Subject.Name name)
        {
            // pull one from reserved
            Subject ret = (Subject)this.PullFromReserved();

            // set values
            ret.name = name;

            // add to the active
            this.Add(ret);

            return ret;
        }

        // Automatically create all the subject needed
        public void CreateAllSubjects()
        {
            this.AddSubject(Subject.Name.LeftKey);
            this.AddSubject(Subject.Name.RightKey);
            this.AddSubject(Subject.Name.SpaceKey);
        }

        // Find by name
        public Subject FindSubjectByName(Subject.Name name)
        {
            Subject ret = null;

            ret = (Subject)this.poActiveHead;
            while(ret != null)
            {
                if (ret.name == name)
                    break;

                ret = (Subject)ret.pNext;
            }

            return ret;
        }


        // Update all subjects
        public void UpdateSubjects()
        {
            Subject current = (Subject)this.poActiveHead;
            while(current != null)
            {
                current.Update();
                current = (Subject)(current.pNext);
            }
        }

        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Subject();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Subject();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
