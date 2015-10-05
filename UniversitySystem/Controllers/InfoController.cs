using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web;
using System.Web;
using UniversitySystem.Entities;
using UniversitySystem.Repositories;
using UniversitySystem.ViewModel.Infos;
using System.Web.Hosting;
using System.Configuration;

namespace UniversitySystem.Controllers
{
    [Filter.AuthenticationFilter]
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InfoPage()
        {
            InfoInfoPageVM model = new InfoInfoPageVM();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            this.TryUpdateModel(model);

            InfoRepository infoRepository = new InfoRepository();

            List<Info> filteredInfo = new List<Info>();
            if (model.Search == null)
            {
                filteredInfo = infoRepository.GetAll();
            }
            else
            {
                filteredInfo = infoRepository.GetAll(filter: x => x.Keywords.Any(k => k.Value.Contains(model.Search)));
            }

            model.Search = model.Search == null ? model.Search : model.Search.Trim(' ');

            model.Info = filteredInfo;

            if ((Models.AuthenticationManager.LoggedUser.GetType().Name).Split('_').GetValue(0).ToString() == "Administrator")
            {
                model.CanBeEdited = true;
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditInfo(int? id)
        {
            InfoEditInfoVM model = new InfoEditInfoVM();
            InfoRepository infoRepository = new InfoRepository();
            Info info = new Info();

            model.InfoImages = new List<InfoImage>();

            if (id != null)
            {
                info = infoRepository.GetByID(id.Value);

                if (info == null)
                {
                    return RedirectToAction("InfoPage", "Info");
                }

                model.KeywordsInput = string.Empty;

                if (info.Keywords.Count() > 0)
                {
                    model.KeywordsInput = string.Join(", ", info.Keywords.OrderBy(o => o.Value).Select(x => x.Value));
                }

                model.ID = id.Value;
                model.Title = info.Title;
                model.Content = info.Content;
                model.KeywordsInput = string.Join(", ", info.Keywords.OrderBy(o => o.Value).Select(x => x.Value));
                model.InfoImages = info.Images;
                model.FilePath = "/Content/Files/" + model.Title;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult EditInfo(InfoEditInfoVM model)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            InfoRepository infoRepository = new InfoRepository(unitOfWork);
            Info info = new Info();

            if (infoRepository.GetAll(filter: x => x.Title == model.Title && x.ID != model.ID).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Title", "Article with same name exist");
            }

            model.Title = model.Title.Trim();

            string message = "Successfully created ";

            if (model.ID > 0)
            {
                info = infoRepository.GetByID(model.ID);
                message = "Successfully edited ";
                if (info.Title != model.Title)
                {
                    string filesLocalPath = "~/Content/Files/" + model.Title;
                    if (Directory.Exists(HostingEnvironment.MapPath(filesLocalPath + info.Title)))
                    {
                        Directory.Move(HostingEnvironment.MapPath(filesLocalPath + info.Title),
                                       HostingEnvironment.MapPath(filesLocalPath + model.Title));
                    }
                }
            }

            model.Content = model.Content.Trim();

            info.Content = model.Content;
            info.Title = model.Title;

            infoRepository.Save(info);

            InfoImageRepository infoImageRepository = new InfoImageRepository(unitOfWork);

            HttpFileCollection UploadedFiles = System.Web.HttpContext.Current.Request.Files;

            string filePath = "~/Content/Files/" + model.Title + "/";

            Regex imageRegex = new Regex(ConfigurationManager.AppSettings["ImageRestriction"]);
            Regex fileRegex = new Regex(ConfigurationManager.AppSettings["FileRestriction"]);

            for (int i = 0; i < UploadedFiles.Count; i++)
            {
                if (imageRegex.Match(UploadedFiles[i].FileName).Success)
                {
                    InfoImage infoImage = new InfoImage();

                    infoImage.Name = UploadedFiles[i].FileName;

                    if (infoImageRepository.GetAll(filter: x => x.Name == infoImage.Name && x.InfoID == info.ID).FirstOrDefault() == null)
                    {
                        info.Images = info.Images == null ? info.Images = new List<InfoImage>() : info.Images;
                        info.Images.Add(infoImage);

                        //Create  folder
                        if (!Directory.Exists(HostingEnvironment.MapPath(filePath)))
                            Directory.CreateDirectory(HostingEnvironment.MapPath(filePath));

                        UploadedFiles[i].SaveAs(HostingEnvironment.MapPath(filePath) + UploadedFiles[i].FileName);
                    }
                }
                else if (fileRegex.Match(UploadedFiles[i].FileName).Success)
                {
                    var file = UploadedFiles[i];

                    //Create  folder
                    if (!Directory.Exists(HostingEnvironment.MapPath(filePath)))
                        Directory.CreateDirectory(HostingEnvironment.MapPath(filePath));

                    //If other file is uploaded we delete the old
                    if (info.FileName != null && System.IO.File.Exists(HostingEnvironment.MapPath(filePath + info.FileName)) && file.FileName != info.FileName)
                        System.IO.File.Delete(HostingEnvironment.MapPath(filePath + info.FileName));

                    info.FileName = file.FileName;

                    file.SaveAs(HostingEnvironment.MapPath(filePath) + file.FileName);
                }
            }

            infoRepository.Save(info);
            unitOfWork.Commit();

            AddRemoveKeywords(model.KeywordsInput, info.ID);

            return RedirectToAction("InfoPage", "Info");
        }

        public ActionResult DeleteInfo(int? id)
        {
            if (id != null)
            {
                InfoRepository InfoRepository = new InfoRepository();
                Info info = InfoRepository.GetByID(id.Value);

                InfoRepository.Delete(info);
                string folderPath = "~/Content/Files/" + info.Title;
                if (Directory.Exists(HostingEnvironment.MapPath(folderPath)))
                    Directory.Delete(HostingEnvironment.MapPath(folderPath), true);
            }

            return RedirectToAction("InfoPage", "Info");
        }


        public JsonResult DeleteFile(int infoid, int imageid)
        {
            InfoRepository infoRepository = new InfoRepository();
            Info info = infoRepository.GetByID(infoid);
            string deleted = string.Empty;

            string filePath = "~/Content/Files/" + info.Title + "/";

            if (imageid > 0)
            {
                deleted = "image";

                InfoImageRepository infoImageRepository = new InfoImageRepository();
                InfoImage infoImage = infoImageRepository.GetByID(imageid);

                infoImageRepository.Delete(infoImageRepository.GetByID(imageid));

                if (infoImage != null && System.IO.File.Exists(HostingEnvironment.MapPath(filePath + infoImage.Name)))
                {
                    System.IO.File.Delete(HostingEnvironment.MapPath(filePath + infoImage.Name));
                }
            }
            return Json(deleted);
        }

        /// <summary>
        /// This method is called to add or remove Keyword from article
        /// </summary>
        public void AddRemoveKeywords(string keywords, int infoID)
        {

            UnitOfWork unitOfWork = new UnitOfWork();
            KeywordsRepository keywordsRepository = new KeywordsRepository(unitOfWork);
            InfoRepository infoRepository = new InfoRepository(unitOfWork);

            Info info = infoRepository.GetByID(infoID);

            List<int> KeywordsIDList = new List<int>();
            if (keywords != null)
            {
                string[] keywordsArray = keywords.Split(',');

                foreach (var keywordInput in keywordsArray)
                {
                    string value = keywordInput.Trim();
                    value = Regex.Replace(value, @"\s+", " ");

                    Keyword keyword = keywordsRepository.GetAll(filter: x => x.Value == value).FirstOrDefault();

                    if (keyword == null)
                    {
                        if (value != string.Empty)
                        {
                            keyword = new Keyword();
                            keyword.Value = value;
                            keywordsRepository.Save(keyword);

                            KeywordsIDList.Add(keyword.ID);
                        }
                    }
                    else
                    {
                        KeywordsIDList.Add(keyword.ID);
                    }
                }
            }

            List<int> currentArticleKeywordsID = new List<int>();
            info.Keywords = info.Keywords == null ? info.Keywords = new List<Keyword>() : info.Keywords;

            try
            {
                foreach (var item in info.Keywords)
                {
                    currentArticleKeywordsID.Add(item.ID);
                }

                IEnumerable<int> removedKeywordsID = currentArticleKeywordsID.Except(KeywordsIDList);

                foreach (Keyword keyword in info.Keywords.ToList().Where(x => removedKeywordsID.Any(k => k == x.ID)))
                {
                    keyword.Info.Remove(info);
                    keywordsRepository.Save(keyword);
                }

                IEnumerable<int> addKeywordToArticle = KeywordsIDList.Except(currentArticleKeywordsID);

                foreach (var keyID in addKeywordToArticle)
                {
                    Keyword keyword = keywordsRepository.GetByID(keyID);
                    keyword.Info = keyword.Info == null ? new List<Info>() : keyword.Info;
                    keyword.Info.Add(info);

                    keywordsRepository.Save(keyword);
                }
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                throw ex;
            }
        }
    }
}
