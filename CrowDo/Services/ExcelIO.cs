//using CrowDo.Models;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using CrowDo.Core.Data;

//namespace CrowDo.Services
//{
//    public class ExcelIO:IExcelIO
//    {
//        private CrowDoDbContext context;

//        public ExcelIO(CrowDoDbContext dbContext)
//        {
//            context = dbContext;
//        }
//        public List<User> ReadExcel(string filename)
//        {
//            XSSFWorkbook hssfwb;
//            try
//            {
//                using (FileStream file = new FileStream(filename,
//                    FileMode.Open, FileAccess.Read))
//                {
//                    hssfwb = new XSSFWorkbook(file);
//                }
//                ISheet sheet = hssfwb.GetSheet("User");
//                var users = new List<User>();
//                for (int row = 0; row <= sheet.LastRowNum; row++)
//                {
//                    if (sheet.GetRow(row) != null)
//                    //null is when the row only contains empty cells
//                    {
//                        User u = new User
//                        {
//                            FirstName = sheet.GetRow(row).GetCell(0).StringCellValue,
//                            Email = sheet.GetRow(row).GetCell(2).StringCellValue
//                        };
//                        users.Add(u);
//                    }
//                }

//                context.Set<User>().AddRange(users);
//                context.SaveChanges();
//                return users;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//    }
//}
