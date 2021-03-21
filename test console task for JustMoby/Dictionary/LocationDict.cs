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
        public List <NPCDict> NPCInLocation { get; private set; }

        public LocationDict(JSONNode node) {
            Id = node["id"].AsInt;
            LocName = node["locName"].Value;
            LocDescription = node["locDesc"].Value;
            LocationsIdForPlayerMove = new List<int>();
            foreach (JSONNode doc in node["LocationsIdForPlayerMove"].AsArray) {
                LocationsIdForPlayerMove.Add(doc.AsInt);
            }
        }

        public void AddNPC(NPCDict npc) {
            if(NPCInLocation==null) NPCInLocation = new List<NPCDict>();
            NPCInLocation.Add(npc);
        }
    }
}
