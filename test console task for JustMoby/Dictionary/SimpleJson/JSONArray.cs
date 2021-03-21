using System.Collections;
using System.Collections.Generic;

namespace SimpleJson
{
    public class JSONArray : JSONNode, IEnumerable<JSONNode>
    {
        private List<JSONNode> m_List;

        public JSONArray() : this(true)
        {
        }

        public JSONArray(int capacity) : this(true, capacity)
        {
        }

        internal JSONArray(bool init, int capacity = 0)
        {
            if (init) Init(capacity); 
        }

        internal void Init(int capacity)
        {
             m_List = new List<JSONNode>(capacity);
        }

        public override JSONNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= m_List.Count)
                    return new JSONLazyCreator(this);
                return m_List[aIndex];
            }
            set
            {
                if (aIndex < 0 || aIndex >= m_List.Count)
                    m_List.Add(value);
                else
                    m_List[aIndex] = value;
            }
        }

        public override JSONNode this[string aKey]
        {
            get { return new JSONLazyCreator(this); }
            set { m_List.Add(value); }
        }

        public override int Count
        {
            get { return m_List.Count; }
        }

        public override void Add(string aKey, JSONNode aItem)
        {
            m_List.Add(aItem);
        }

        public override JSONNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= m_List.Count)
                return null;
            JSONNode tmp = m_List[aIndex];
            m_List.RemoveAt(aIndex);
            return tmp;
        }

        public override JSONNode Remove(JSONNode aNode)
        {
            m_List.Remove(aNode);
            return aNode;
        }

        public IEnumerator<JSONNode> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        public override System.Text.StringBuilder ToStringBuilder(System.Text.StringBuilder builder)
        {
            builder.Append("[");
            if (m_List != null)
            {
                bool first = true;
                foreach (JSONNode n in m_List)
                {
                    if (!first) builder.Append(",");
                    first = false;
                    n.ToStringBuilder(builder);
                }
            }
            builder.Append("]");
            return builder;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Serialize(System.IO.BinaryWriter aWriter)
        {
            aWriter.Write((byte) JSONBinaryTag.Array);
            aWriter.Write(m_List.Count);
            for (int i = 0; i < m_List.Count; i++)
            {
                m_List[i].Serialize(aWriter);
            }
        }
    } // End of JSONArray
}