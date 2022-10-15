using System;
using System.IO;
using System.Web.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            var Result = new JsonResult();
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("/content/images/"), fileName);
                file.SaveAs(path);
                Result.Data = new
                {
                    Success = true,
                    ImageURL = string.Format("/content/images/{0}", fileName),
                    JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                Result.Data = new { Success = false, Message = ex.Message, JsonRequestBehavior.AllowGet };
            }

            return Result;
        }
    }
}