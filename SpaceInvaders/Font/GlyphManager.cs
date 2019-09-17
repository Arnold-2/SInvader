using System;
using System.Diagnostics;
using System.Xml;


namespace SpaceInvaders
{
    // <singleton>
    public class GlyphManager : Manager
    {
        private static GlyphManager privInstance = null;

        public GlyphManager() : base(5, 3)
        {
        }

        public static GlyphManager getInstance()
        {
            if (privInstance == null)
            {
                privInstance = new GlyphManager();
            }

            return privInstance;
        }

        public Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            Glyph pNode = (Glyph)this.PullFromReserved();
            Debug.Assert(pNode != null);

            pNode.Set(name, key, textName, x, y, width, height);
            this.Add(pNode);
            return pNode;
        }

        public Glyph FindGlyph(Glyph.Name _name, int _key)
        {
            // Compare functions only compares two Nodes
            Glyph current = (Glyph)(this.poActiveHead);

            while (current != null)
            {
                if (current.name == _name && current.key == _key)
                    return current;

                current = (Glyph)(current.pNext);
            }

            return null;
        }

        

        public void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // I'm sure there is a better way to do this... but this works for now
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // have all the data... so now create a glyph
                            //Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            this.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }


        // Add given number of Nodes into an empty Reserved List. 
        // Called when constructing manager or growing the size
        public override void CreateReservedNodes(int numofNodes)
        {
            DLink tmpNode = new Glyph();
            this.poReservedHead = tmpNode;
            int cnt = 1;

            while (cnt < numofNodes)
            {
                tmpNode.pNext = new Glyph();
                tmpNode.pNext.pPrev = tmpNode;
                tmpNode = tmpNode.pNext;
                cnt++;
            }

            this.mNumReserved = numofNodes;
        }
    }
}
