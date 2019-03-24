using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFileUploads.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            //var items = GetFiles();
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                   
                    var fileName = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    if (extension == ".pdf")
                    {
                        var filePath = Path.Combine(Server.MapPath("~/AquivosPDF"), fileName);
                        file.SaveAs(filePath);
                     
                        ViewBag.Status = "OK";
                        ViewBag.msg = "Arquivo salvo com sucesso!";

                    }
                    else
                    {
                        ViewBag.Status = "NG";
                        ViewBag.msg = "Falha ao savar o arquivo. Tipo de arquivo não é um PDF! ";
                    }

                }
                //var items = GetFiles();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Status = "NG";
                ViewBag.msg = "Arquivo não salvo! " + ex.Message;
                return View();
            }

        }

        //lista os arquivos
        private List<string> GetFiles()
        {
            //var dir = new System.IO.DirectoryInfo(Server.MapPath("~/ArquivosPDF"));
            //System.IO.FileInfo[] fileNames = dir.GetFiles(".pdf");
            DirectoryInfo diretorio = new DirectoryInfo(Server.MapPath("~/ArquivosPDF/"));
            FileInfo[] arquivos = diretorio.GetFiles();
            List<string> items = new List<string>();
            foreach(var file in arquivos)
            {
                items.Add(file.Name);
            }
            return items;
        }

        //Faz o download da imagem
        public FileResult Download(string ImageName)
        {
            var fileVirtualPath = "~/ArquivosPDF/" + ImageName;
            return File(fileVirtualPath, "application/force- download", Path.GetFileName(fileVirtualPath));

        }
    }
}