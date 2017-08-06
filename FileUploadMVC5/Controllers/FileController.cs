using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FileController : Controller
    {
        /// <summary>
        /// 一開始進入表單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ImagesModel());
        }

        /// <summary>
        /// 表單提交
        /// </summary>
        /// <param name="act"></param>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string act, string actionType, string actionName, ImagesModel vm, List<HttpPostedFileBase> images)
        {
            switch (act)
            {
                case "upload"://上傳照片
                    this.UploadPhoto(vm, images);
                    break;

                case "post"://存檔，寫DB
                    this.SaveImagesToDB(vm);
                    break;
            }
            return View(vm);
        }

        /// <summary>
        /// 寫Log查看表單post的結果
        /// </summary>
        /// <param name="vm"></param>
        private void SaveImagesToDB(ImagesModel vm)
        {
            //寫Log - 檔案名稱(知道上傳的檔案名稱，就可以寫DB了)
            using (FileStream fs = new FileStream(@"D:\myLog.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);

                foreach (var img in vm.MemberData)
                {
                    sw.WriteLine(img.FileName);
                }
                sw.Close();
            }
        }

        /// <summary>
        /// 上傳照片
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        private void UploadPhoto(ImagesModel vm, List<HttpPostedFileBase> images)
        {
            if (images != null && images.Count > 0)//使用者有選擇照片檔案
            {
                for (int i = 0; i < images.Count; i++)
                {
                    string strFileName = Guid.NewGuid().ToString() + Path.GetExtension(images[i].FileName);
                    string strFilePath = Server.MapPath("~/Content/FileUpload/Images/" + strFileName);
                    images[i].SaveAs(strFilePath);

                    vm.MemberData.Add(new MemberViewModel()
                    {
                        FileName = strFileName,
                        FileUrl = @"http://localhost:19233/" + "Content/FileUpload/Images/" + strFileName,
                    });
                }
            }
        }
    }
}