using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class UndergroundLine
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Station> Stations { get; set; }

        public int SomeProp { get; set; }

        public UndergroundLine(string name, List<Station> stations, int prop)
        {
            this.Name = name;
            this.Stations = stations;
            this.SomeProp = prop;
        }

        public UndergroundLine()
        {
            this.Name = "default";
            this.Stations = new List<Station>();
            this.SomeProp = 0;
        }

        public override string ToString()
        {
            string str = null;

            foreach (var item in Stations)
            {
                str += item.ToString() + "\n";
            }
            return Name + "\n" + str;
        }
    }
}
