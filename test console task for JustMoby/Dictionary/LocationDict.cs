using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestForJustMoby.Dictionary
{
    public class LocationDict
    {
        public int Id { get; private set; }
        public string LocName { get; private set; }
        public string LocDescription { get; private set; }
        public List<int> LocationsIdForPlayerMove { get; private set; }
        public List<NPCDict> NPCInLocation { get; private set; }

        public LocationDict(JSONNode node)
        {
            Id = node[_jsonId].AsInt;
            LocName = node[_jsonLocName].Value;
            LocDescription = node[_jsonLocDesc].Value;
            LocationsIdForPlayerMove = new List<int>();
            foreach (JSONNode doc in node[_jsonLocIdForMove].AsArray)
            {
                LocationsIdForPlayerMove.Add(doc.AsInt);
            }
        }

        public void AddNPC(NPCDict npc)
        {
            if (NPCInLocation == null) NPCInLocation = new List<NPCDict>();
            NPCInLocation.Add(npc);
        }

        private const string _jsonId = "id";
        private const string _jsonLocName = "locName";
        private const string _jsonLocDesc = "locDesc";
        private const string _jsonLocIdForMove = "LocationsIdForPlayerMove";
    }
}
