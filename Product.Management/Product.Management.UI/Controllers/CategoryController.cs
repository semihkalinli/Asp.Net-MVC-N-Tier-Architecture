using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Product.Management.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("kategoriler")]
        public ActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                return View();
            }
            else
            {
                int draw = Convert.ToInt32(Request.Form["draw"]);  // etkin sayfa numarası
                int start = Convert.ToInt32(Request.Form["start"]);   //listenen ilk kayıtın  index numarası
                int length = Convert.ToInt32(Request.Form["length"]);   //sayfadaki toplam listelenecek kayit sayısı
                string search = Request.Form["search[value]"];   //search filter
                string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];  //Sıralama yapılacak column adı
                string sortColumnDir = Request.Form["order[0][dir]"];   //sıralama türü
                int totalRecords = 0;
                string orderby = "";


                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))   //filter
                    orderby = sortColumn + " " + sortColumnDir;

                Tuple<IEnumerable<Categories>, int> dataPagingList = _categoryRepository.GetCategoryList(start, length, orderby, search);

                totalRecords = dataPagingList.Item2;
                return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = dataPagingList.Item1 });
            }
        }

        [Route("kategori-ekle")]
        public ActionResult Add()
        {
            ViewBag.Response = TempData["ResponseMessage"];
            return View();
        }
        [Route("kategori-duzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.Response = TempData["ResponseMessage"];
            CategoryViewModel model = new CategoryViewModel
            {
                Category = _categoryRepository.DetailCategory(id)
            };
            return View(model);
        }
        [AcceptVerbs("POST")]
        public ActionResult CategoryFormAdd(CategoryFormAddViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Categories categoryForm = new Categories
                    {
                        Name = form.Name,
                        Description = form.Description
                    };
                    var response = _categoryRepository.InsertCategory(categoryForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Kategori başarıyla eklendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Kategori eklenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/kategori-ekle");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [AcceptVerbs("POST")]
        public ActionResult CategoryFormEdit(CategoryFormEditViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Categories categoryForm = new Categories
                    {
                        Id=form.Id,
                        Name = form.Name,
                        Description = form.Description
                    };
                    var response = _categoryRepository.UpdateCategory(categoryForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Kategori başarıyla güncellendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Kategori güncellenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/kategori-duzenle?id="+form.Id);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}