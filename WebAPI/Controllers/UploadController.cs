using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Web;
using Detention_facility.Custom;


namespace WebAPI.Controllers
{


    public class UploadController : ApiController
    {

        [Authorize(Roles = "Admin, Editor, User")]
        [HttpPost]
        public IHttpActionResult UploadJsonFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            string fileNamesList = "";
            bool flag = false;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    if (File.Exists(filePath))
                    {
                        flag = true;
                        if (!fileNamesList.Contains(postedFile.FileName))
                            fileNamesList = fileNamesList + postedFile.FileName + " ";                      
                    }   
                }
                if (flag)
                {

                   var badRequest = new ResponseClass<string>
                   {
                       IsSuccess = false,
                       Message = "На сервере имеются файлы с именами:" + fileNamesList,
                       ResponseData = null
                   };

                   return Ok(badRequest);
                }
                else
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);

                        if (!fileNamesList.Contains(postedFile.FileName))
                            fileNamesList = fileNamesList + postedFile.FileName + " ";
                    }
            }

            var goodRequest = new ResponseClass<string>
            {
                IsSuccess = true,
                Message = "На сервер добавлены файлы:" + fileNamesList,
                ResponseData = null
            };

            return Ok(goodRequest);
        }
    }
}