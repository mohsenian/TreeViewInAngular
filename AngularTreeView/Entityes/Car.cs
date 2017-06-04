using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularTreeView.Entityes
{
    public class Car
    {
        //Auto generate id for car 
        public Car()
        {
            myID++;
            this.Id = myID;
        }
        private static int myID = 0;
        private int Id = 0;

        public int ID
        {
            get { return Id; }
        }

        public string Company { get; set; }
        public string CarTag { get; set; }
        public int ParentID { get; set; }
        public int ChildID { get; set; }

        public List<Car> Childrens { get; set; }


    }

}

