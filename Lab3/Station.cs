using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SomeProp1 { get; set; }
        public int SomeProp2 { get; set; }

        public Station(string name, int prop1, int prop2)
        {
            this.Name = name;
            this.SomeProp1 = prop1;
            this.SomeProp2 = prop2;
        }

        public Station()
        {
            this.Name = "default";
            this.SomeProp1 = 1;
            this.SomeProp2 = 2;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
