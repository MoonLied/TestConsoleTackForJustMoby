using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleJson
{
    public class JSONClass : JSONNode, IEnumerable<KeyValuePair<string, JSONNode>>
    {
        private Dictionary<string, JSONNode> m_Dict;

        public JSONClass() : this(true)
        {
        }

        internal JSONClass(bool init)
        {
            if (init) Init(0);
        }

        internal void Init(int capacity)
        {
            m_Dict = new Dictionary<string, JSONNode>(capacity);
        }

        public override JSONNode this[string aKey]
        {
            get
            {
                JSONNode ret = null;
                if (!m_Dict.TryGetValue(aKey, out ret)) ret = new JSONLazyCreator(this, aKey);
                return ret;
            }
            set
            {
                m_Dict[aKey] = value;
            }
        }

        public override JSONNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= m_Dict.Count)
                    return null;
                return m_Dict.ElementAt(aIndex).Value;
            }
            set
            {
                if (aIndex < 0 || aIndex >= m_Dict.Count)
                    return;
                string key = m_Dict.ElementAt(aIndex).Key;
                m_Dict[key] = value;
            }
        }

        public override int Count
        {
            get { return m_Dict.Count; }
        }


        public override void Add(string aKey, JSONNode aItem)
        {
            if (!string.IsNullOrEmpty(aKey))
                m_Dict[aKey] = aItem;
            else
                m_Dict.Add(Guid.NewGuid().ToString(), aItem);
        }

        public override JSONNode Remove(string aKey)
        {
            if (!m_Dict.ContainsKey(aKey))
                return null;
            JSONNode tmp = m_Dict[aKey];
            m_Dict.Remove(aKey);
            return tmp;
        }

        public override JSONNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= m_Dict.Count)
                return null;
            var item = m_Dict.ElementAt(aIndex);
            m_Dict.Remove(item.Key);
            return item.Value;
        }

        public override JSONNode Remove(JSONNode aNode)
        {
            try
            {
                var item = m_Dict.Where(k => k.Value == aNode).First();
                m_Dict.Remove(item.Key);
                return aNode;
            }
            catch
            {
                return null;
            }
        }
        
        public IEnumerator<KeyValuePair<string, JSONNode>> GetEnumerator()
        {
            return m_Dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerable<string> GetKeys()
        {
            return m_Dict.Keys;
        }

        public static implicit operator string(JSONClass d)
        {
            return (d == null) ? null : d.ToString();
        }

        public override System.Text.StringBuilder ToStringBuilder(System.Text.StringBuilder builder)
        {
            builder.Append("{");
            if (m_Dict != null)
            {
                bool first = true;
                foreach (KeyValuePair<string, JSONNode> n in m_Dict)
                {
                    if (!first) builder.Append(",");
                    first = false;
                    builder.Append("\"");
                    EscapeToBuilder(builder, n.Key);
                    builder.Append("\":");
                    n.Value.ToStringBuilder(builder);
                }
            }
            builder.Append("}");
            return builder;
        }

        public string ToSortedString()
        {
            var builder = new System.Text.StringBuilder();
            builder.Append("{");
            bool first = true;
            var list = m_Dict.Keys.ToList();
            list.Sort();
            for (var i = 0; i < list.Count; ++ i)
            {
                var key = list[i];
                if (!first) builder.Append(",");
                first = false;
                builder.Append("\"");
                EscapeToBuilder(builder, key);
                builder.Append("\":");
                m_Dict[key].ToStringBuilder(builder);
            }
            builder.Append("}");
            return builder.ToString();
        }

        public override void Serialize(System.IO.BinaryWriter aWriter)
        {
            aWriter.Write((byte)JSONBinaryTag.Class);
            aWriter.Write(m_Dict.Count);
            foreach (string K in m_Dict.Keys)
            {
                aWriter.Write(K);
                m_Dict[K].Serialize(aWriter);
            }
        }

        public override bool ContainsKey(string aKey)
        {
            return m_Dict.ContainsKey(aKey);
        }

        public bool TryGetValue(string aKey, out JSONNode node)
        {
            node = null;

            if (!m_Dict.ContainsKey(aKey)) return false;

            node = m_Dict[aKey];
            return true;
        }


        public override long AsLong { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override ulong AsULong { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override int AsInt { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override uint AsUInt { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override float AsFloat { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override double AsDouble { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public override bool AsBool { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
    } // End of JSONClass
}