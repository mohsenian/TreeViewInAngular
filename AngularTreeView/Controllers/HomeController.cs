using AngularTreeView.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularTreeView.Controllers
{
    public class HomeController : Controller
    {
        static List<Car> caList = new List<Car>() {
        new Car() {Company="Benz",ParentID=0,ChildID=0},
        new Car() {Company="BMW",ParentID=0,ChildID=0},
        new Car() {Company="Nissan",ParentID=0,ChildID=0},
        new Car() {Company="Series-E",ParentID=1,ChildID=1},
        new Car() {Company="Series-C",ParentID=1,ChildID=1},
        new Car() {Company="Series-D",ParentID=1,ChildID=1},
        new Car() {Company="BMW-A-Series",ParentID=2,ChildID=2},
        new Car() {Company="BMW-B-Series",ParentID=2,ChildID=2},
        new Car() {Company="BMW-C-Series",ParentID=2,ChildID=2},
        new Car() {Company="Nissan-Class-A",ParentID=3,ChildID=3},
        new Car() {Company="Nissan-Class-B",ParentID=3,ChildID=3},
        new Car() {Company="Nissan-Class-C",ParentID=3,ChildID=3}};
     
       
        //This variable is for keep child nod id than return from view
        #region static-variable
        public static int postParentId;
        #endregion
        // GET: Home
        public ActionResult Index()
        {
            Car car = new Car();
            car.Childrens = new List<Car>();
            car.Childrens = (from c in caList where c.ParentID == 0 select c).ToList();
         
            return View(car);
        }
        [HttpPost]
        public ActionResult Index(Car myCar,FormCollection formColl)
        {
            //Get Cild id from selected Dropdwonlist Item
            int childId = Convert.ToInt32(formColl["childDropDown"]);
            Car carObj = new Car();
            carObj.Company = myCar.Company;
            carObj.ParentID = childId;
            caList.Add(carObj);

            carObj.Childrens = new List<Car>();
            carObj.Childrens = (from c in caList where c.ParentID == 0 select c).ToList();
            ModelState.Clear();
            return View(carObj);
        }
        public ActionResult Details(int Id)
        {
            List<Car> objCar = new List<Car>();
            objCar = caList.Where(m => m.ChildID == Id).ToList();

            return View(objCar);
        }
        //Action result for ajax call get ParentId from  DropDownList ("CarTag" )and populate another DropDownList("childDropDown")
        //This ActionResult than return Json to JavaScriot file ("populate-dropdown-from-dropdown.js") in Script folder
        public ActionResult GetParentByParentId(int subMainId)
        {
            postParentId = subMainId;
            List<Car> objCar = new List<Car>();
            objCar = caList.Where(m => m.ChildID == subMainId).ToList();
            SelectList carSelect = new SelectList(objCar, "ID", "Company");
            return Json(carSelect);
        }

        public JsonResult GetCarStructure()
        {
            List<Car> treeList = new List<Car>();
            if (caList.Count>0)
            {
                treeList = BuildTree(caList);
            }
            return new JsonResult { Data = new { treeList = treeList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //Recurstion method for recursively get all child nodes
        public void GetTreeView(List<Car>list,Car current,ref List<Car> returnList)
        {
            //Get child of current item
            var childs = list.Where(a => a.ParentID == current.ID).ToList();
            current.Childrens = new List<Car>();
            current.Childrens.AddRange(childs);
            foreach (var i in childs)
            {
                GetTreeView(list, i, ref returnList);
            }
        }
        public List<Car> BuildTree(List<Car> list)
        {
            List<Car> returnList = new List<Car>();
            //Find top level items
            var topLevels = list.Where(a => a.ParentID == list.OrderBy(b => b.ParentID).FirstOrDefault().ParentID);
            returnList.AddRange(topLevels);
            foreach (var i in returnList)
            {
                GetTreeView(list, i, ref returnList);
            }
            return returnList;
        }
    }
}