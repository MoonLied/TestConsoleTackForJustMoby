using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace test_console_task_for_JustMoby.Dictionary
{
    public class LocationModel
    {
        public int Id { get; private set; }
        public string LocName { get; private set; }
        public string LocDescription { get; private set; }
        public List<int> LocationsIdForPlayerMove { get; private set; }
        public List <NPCModel> NPCInLocation { get; private set; }

        public LocationModel(JSONNode node) {
            Id = node["id"].AsInt;
            LocName = node["locName"].Value;
            LocDescription = node["locDesc"].Value;
            LocationsIdForPlayerMove = new List<int>();
            foreach (JSONNode doc in node["LocationsIdForPlayerMove"].AsArray) {
                LocationsIdForPlayerMove.Add(doc.AsInt);
            }
        }

        public void AddNPC(NPCModel npc) {
            if(NPCInLocation==null) NPCInLocation = new List<NPCModel>();
            NPCInLocation.Add(npc);
        }
    }
}
