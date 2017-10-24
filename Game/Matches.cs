using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameProject
{
    [Serializable]
    public class Matches
    {
        //private List<Match> matches;
        [XmlArray("Matches"), XmlArrayItem("Match")]
        public List<Match> Items {
            get;
            //{
                //return matches;
            //}
            set;
        }

        public Matches()
        {
            Items = new List<Match>();
        }

        public Matches(int count)
        {
            Items = new List<Match>(count);
        }

        public void TakeMatchToggle(int index)
        {
            Match targetMatch = Items[index];
            if (targetMatch.Taken == false)
                targetMatch.Taken = true;
            else
                targetMatch.Taken = false;
        }

        public void RemoveTaken()
        {
            Items.RemoveAll(t => t.Taken == true);
        }
    }
}
