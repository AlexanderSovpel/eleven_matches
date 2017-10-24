using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public int Loose { get; set; }

        public Player() { }
        public Player(string name)
        {
            Name = name;
        }
    }

    public class PlayersComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(Player obj)
        {
            return obj.Name.GetHashCode() ^ obj.Win.GetHashCode() ^ obj.Loose.GetHashCode();
        }
    }
}
