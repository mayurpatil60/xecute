using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using xpPortal.BL;
using xpPortal.Models;

namespace xpPortal.Controllers
{
    //[Authorize]
    public class DocumentController : Controller
    {
       
        
        public DocumentController()
        {

        }

        // GET: Login
        public ActionResult UploadDocument()
        {
            return View("UploadDocument");
        }


        [HttpPost]
        public ActionResult UploadDocument(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedDocuments"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        [HttpPost]
        public JsonResult DocumentUpload()
        {
            try
            {///http://www.c-sharpcorner.com/uploadfile/manas1/upload-files-through-jquery-ajax-in-asp-net-mvc/
                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                      
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }                    
                    fname = Session["LoginID"].ToString() +"_"+ Request.Form["ControlName"].ToString() + "_" + fname;
                    string documentLink = fname;
                    // Get the complete folder path and store the file inside it.  
                    fname = Path.Combine(Server.MapPath("~/UploadedDocuments/"), fname);
                    file.SaveAs(fname);
                    BusinessLayer objBl = new BusinessLayer();
                    objBl.InsertIntoTable("insert into document(DocumentName, DocumentType, DocumentLink, UserLoginId, Status) values ('" + Request.Form["ControlName"].ToString() + "', '" + Request.Form["ControlName"].ToString() + "', '" + documentLink + "', " + Session["LoginID"].ToString() + ", 1);");
                    }
                    // Returns message that successfully uploaded  
                    return Json("1");                               
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("0");
            }           
        }

        [HttpGet]
        public ActionResult ViewDocument(string emailID)
        {
            BusinessLayer objBl = new BusinessLayer();
            NewJoinee model = new NewJoinee();
            DataRowCollection drc = objBl.GetDocumentListByUser(emailID);
            model.NewJoineeList = drc;
            return View("ViewDocument", model);
        }

        [HttpGet]
        public string Approvedocument(int documentID)
        {
            BusinessLayer objBl = new BusinessLayer();
            string strResult = "";
            objBl.InsertIntoTable("update document set status = 2 where documentID = "+ documentID);
            return strResult;
        }

        [HttpGet]
        public string RejectDocument(int documentID)
        {
            BusinessLayer objBl = new BusinessLayer();
            string strResult = "";
            objBl.InsertIntoTable("update document set status = 3 where documentID = " + documentID);
            return strResult;
        }
    }


}