using Long_Term_Care.Data;
using Long_Term_Care.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Long_Term_Care.Models.Physicals;

namespace Long_Term_Care.Controllers
{
    public class PhysicalExaminationController : Controller
    {

        private readonly ApplicationDbContext _context;
        public PhysicalExaminationController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult PhysicalExamination()
        {
            List<Physicals> objPhysicalList = _context.Physical.ToList();
            return View(objPhysicalList);

        }


        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Physicals obj)
        {
           
            if (ModelState.IsValid)
            {
                _context.Physical.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("PhysicalExamination");
            }                          
            return View();

        }


        //Edit更改健檢數據內容
        public IActionResult Edit(int? PhysicalId)
        {
            if (PhysicalId == null || PhysicalId == 0)
            {
                return NotFound();
            }
            Physicals PhysicalFromDb = _context.Physical.Find(PhysicalId);
            

            if (PhysicalFromDb == null)
            {
                return NotFound();
            }
            return View(PhysicalFromDb);
        }


        [HttpPost]
        public IActionResult Edit(Physicals obj)
        {

            if (ModelState.IsValid)
            {
                _context.Physical.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("PhysicalExamination");
            }
            return View();
        }


        //Delete刪除整筆資料
        public IActionResult Delete(int? PhysicalId)
        {
            if (PhysicalId == null || PhysicalId == 0)
            {
                return NotFound();
            }
            Physicals PhysicalsFromDb = _context.Physical.Find(PhysicalId);


            if (PhysicalsFromDb == null)
            {
                return NotFound();
            }
            return View(PhysicalsFromDb);
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? PHysicalId)
        {

            if (ModelState.IsValid)
            {

                Physicals? obj = _context.Physical.Find(PHysicalId);
                if (obj == null)
                {
                    return NotFound();
                }
                _context.Physical.Remove(obj);
                _context.SaveChanges();
                return RedirectToAction("PhysicalExamination");
            }
            return View();
        }






        //透過身體數據分析結果


        //設定檢驗數據正常值 min & max 

        public class Examination
        {
            public Double min;
            public Double max;
            public String low_symptom;
            public String high_symptom;


            //建構子
            public Examination(Double min, Double max, String low_symptom, String high_symptom)
            {
                this.min = min;
                this.max = max;
                this.low_symptom = low_symptom;
                this.high_symptom = high_symptom;
            }
        }


        public Dictionary<String, Examination> TestItem = new Dictionary<String, Examination>()
        {
            { "BMI", new Examination(18.5, 24, "營養不良", "肥胖") },
            { "SBP", new Examination(90, 140, "低血壓", "高血壓") },
            { "DBP", new Examination(60, 90, "低血壓", " 高血壓") },
            { "Waist", new Examination(1, 90, "null", "肥胖")},
            { "WBC", new Examination(3.25, 9.16, "免疫低下", "發炎")},
            { "RBC", new Examination(4.21, 5.9, "貧血", "null")},
            { "HB", new Examination(13.1, 17.2, "貧血", "null")},
            { "HCT", new Examination(39.6, 51.5, "貧血", "null")},
            { "PLT", new Examination(150, 378, "貧血", "null")},
            { "HDL", new Examination(40, 999, "高血脂", "null")},
            { "LDL", new Examination(1, 130, "null", "高血脂")},
            { "TG", new Examination(1, 150, "null", "高三酸甘油脂")},
            { "CHOL", new Examination(1, 200, "null", "高膽固醇")},
            { "ALT", new Examination(0, 40, "null", "肝功能不佳")},
            { "AST", new Examination(13, 39, "null", "肝功能不佳")},
            { "CREA", new Examination(0.7, 1.3, "null", "腎功能不佳")},
            { "BUN", new Examination(7, 25, "營養不良", "腎功能不佳")},
            { "Ca", new Examination(2.2, 2.6, "骨質疏鬆", "null")},
            { "HbA1C", new Examination(4, 6, "營養不良", "糖尿病")},
            { "GLU", new Examination(70, 100, "營養不良", "高血糖")},
            { "UIBC", new Examination(155, 355, "缺鐵", "null")},
            { "Fe", new Examination(50, 212, "缺鐵", "null")},
        };

        //之後優化可做 get data from database
        
        //array or List<>放tag(有就放 沒有不放)



        //fake data for testing
        public Dictionary<string, double> fakeData = new Dictionary<string, double>()
        {
            { "BMI", 28 },
            { "SBP", 123 },
            { "DBP", 77 },
            { "Waist", 78 },
            { "WBC", 4.6 },
            { "RBC", 10.32 },
            { "HB", 12.7 },
            { "HCT", 48.1 },
            { "PLT", 267 },
            { "HDL", 100 },
            { "LDL", 230},
            { "TG", 105 },
            { "CHOL", 290 },
            { "ALT", 24 },
            { "AST", 26 },
            { "CREA", 0.8 },
            { "BUN", 21 },
            { "CA", 2.2 },
            { "HbA1C", 7},
            { "GLU", 120 },
            { "UIBC", 223 },
            { "Fe", 201 },
        };



        //判斷數值對應到的tag(症狀)
        public String getTag(String TestItems, Double ClientData)
        {

            Examination match = TestItem[TestItems];
            if (ClientData > match.max)
            {
                return match.high_symptom;

            }
            else if (ClientData < match.min)
            {
                return match.low_symptom;
            }
            return "null";
        }

        public void getData()
        {
            getAllRecommand(fakeData);
        }




        //low_symptom & high_symptom以Dictionary存對應的保健品List

        public Dictionary<String, List<int>> ImproveHealth = new Dictionary<String, List<int>>()
            {
                {"營養不良", new List<int> {2}},
                {"肥胖", new List<int> {11,13}},
                {"低血壓", new List<int> {}},
                {"高血壓", new List<int> {14}},
                {"免疫低下", new List<int> {1}},
                {"發炎", new List<int> {1}},
                {"貧血", new List<int> {2}},
                {"高血脂", new List<int> {7,11}},
                {"高三酸甘油脂", new List<int> {7}},
                {"高膽固醇", new List<int> {9}},
                {"肝功能不佳", new List<int> {11,14}},
                {"腎功能不佳", new List<int> {6,8}},
                {"骨質疏鬆", new List<int> {4,5,8}},
                {"糖尿病", new List<int> {1,13}},
                {"高血糖", new List<int> {13}},
                {"缺鐵", new List<int> {2}},

            };

        //將對應到的症狀
        public List<int> recommand(String TestItems, Double ClientData)
        {
            //邊可以改看看 可以如何重新設計
            String match = getTag(TestItems, ClientData);
            if (match != "null") //match 有可能會get到null字串，因為TestItem裡有null 字串，如果把null 丟到ImprovcHcalth 裡會找不到
                //因為ImproveHealth key 裡面沒有null選項
                return ImproveHealth[match]; 
            else
                return null;
        }

        public List<int> getAllRecommand(Dictionary<String, Double> allItem)
        {
            List<int> allRecommand = new List<int>();
            foreach (KeyValuePair<String, Double> kvp in allItem)
            {
                List<int> temp = recommand(kvp.Key, kvp.Value);
                if (temp != null)
                {
                    foreach (int id in temp)
                    {
                        //如果不包含對應的項目，就將符合的項目放進List中
                        if (!allRecommand.Contains(id))
                        {
                            allRecommand.Add(id);
                        }
                    }
                }
            }
            

            return allRecommand;
            
        }

      



        public IActionResult AnalyzeResult(int? physicalId) //this is null
        {
            //return View();

            Physicals PhysicalFromDb = _context.Physical.FirstOrDefault(u => u.PhysicalId == physicalId);

            if (PhysicalFromDb == null)
            {
                return NotFound();
            }
            //else
            //{
            //    return View(PhysicalFromDb);
            //}
            
            //ViewBag.recommand = getAllRecommand(fakeData);

            //List<string> allRecommand = getAllRecommand(allItem);
    
            Dictionary<string, double> userData = new Dictionary<string, double>();
            userData.Add("BMI", PhysicalFromDb.Weight / Math.Pow((PhysicalFromDb.Height / 100), 2));
            userData.Add("SBP", PhysicalFromDb.SBP);
            userData.Add("DBP", PhysicalFromDb.DBP);
            userData.Add("Waist", PhysicalFromDb.Waist);
            userData.Add("WBC", PhysicalFromDb.WBC);
            userData.Add("RBC", PhysicalFromDb.RBC);
            userData.Add("HB", PhysicalFromDb.HB);
            userData.Add("HCT", PhysicalFromDb.HCT);
            userData.Add("PLT", PhysicalFromDb.PLT);
            userData.Add("HDL", PhysicalFromDb.HDL);
            userData.Add("LDL", PhysicalFromDb.LDL);
            userData.Add("TG", PhysicalFromDb.TG);
            userData.Add("CHOL", PhysicalFromDb.CHOL);
            userData.Add("ALT", PhysicalFromDb.ALT);
            userData.Add("AST", PhysicalFromDb.AST);
            userData.Add("CREA", PhysicalFromDb.CREA);
            userData.Add("BUN", PhysicalFromDb.BUN);
            userData.Add("Ca", PhysicalFromDb.Ca);
            userData.Add("HbA1C", PhysicalFromDb.HbA1C);
            userData.Add("GLU", PhysicalFromDb.GLU);
            userData.Add("UIBC", PhysicalFromDb.UIBC);
            userData.Add("Fe", PhysicalFromDb.Fe);

            List<int> recommands = getAllRecommand(userData);
            List<HealthSupplement> healthSupplements = new List<HealthSupplement>();
            foreach (int id in recommands)
            {
                HealthSupplement healthSupplement = _context.healthSupplements.FirstOrDefault(u => u.SupplementId == id);
                if (healthSupplement != null)
                {
                    healthSupplements.Add(healthSupplement);
                }
            }
            return View(healthSupplements);


            //-----End------

            // Return 之後下面都執行不到喔
            //if (PhysicalId == 1)
            //{
            //    return RedirectToAction("AnaylyzeResultSuccess");
            //}
            //else
            //{
            //    return NotFound();
            //}
        }



        [HttpPost]
        public IActionResult AnaylyzeResultPOST(int? HealthSupplementId)
        {

            if (HealthSupplementId == null || HealthSupplementId == 0)
            {
                return NotFound();
            }

            //Category categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (fakeData == null)
            {
                return NotFound();
            }
            return View("HealthSupplement");
           
        }

        public IActionResult Details(int? id) 
        {
            //HealthSupplement HealthSupplements = _context.HealthSupplements.FirstOrDefault(u => u.SupplementId == id);
            return View();
        }

    }
}


