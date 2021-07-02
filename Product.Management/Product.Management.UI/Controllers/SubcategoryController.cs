using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Product.Management.UI.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        public SubcategoryController(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }
        [AcceptVerbs("GET", "POST")]
        [Route("alt-kategoriler")]
        public ActionResult Index(string id="0", string categoryName="Tümü")
        {
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                ViewBag.Id = id;
                ViewBag.CategoryName = categoryName;
                CategoryViewModel model = new CategoryViewModel
                {
                    Categories = _subcategoryRepository.GetSelectListCategories()
                };
                return View(model);
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

                Tuple<IEnumerable<Subcategories>, int> dataPagingList = _subcategoryRepository.GetSubcategoriesList(int.Parse(id), start, length, orderby, search);

                totalRecords = dataPagingList.Item2;
                return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = dataPagingList.Item1 });
            }
        }
        [Route("alt-kategori-ekle")]
        public ActionResult Add()
        {
            ViewBag.Response = TempData["ResponseMessage"];
            SubcategoryViewModel model = new SubcategoryViewModel
            {
                Categories = _subcategoryRepository.GetSelectListCategories()
            };
            return View(model);
        }
        [Route("alt-kategori-duzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.Response = TempData["ResponseMessage"];
            SubcategoryViewModel model = new SubcategoryViewModel
            {
                Subcategory = _subcategoryRepository.DetailSubcategory(id),
                Categories = _subcategoryRepository.GetSelectListCategories()
            };
            return View(model);
        }
        [AcceptVerbs("POST")]
        public ActionResult SubcategoryFormAdd(SubcategoryFormAddViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Subcategories categoryForm = new Subcategories
                    {
                        CategoryId=form.CategoryId,
                        Name = form.Name,
                        Description = form.Description
                    };
                    var response = _subcategoryRepository.InsertSubcategory(categoryForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Alt Kategori başarıyla eklendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Alt Kategori eklenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/alt-kategori-ekle");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [AcceptVerbs("POST")]
        public ActionResult SubcategoryFormEdit(SubcategoryFormEditViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Subcategories categoryForm = new Subcategories
                    {
                        Id = form.Id,
                        CategoryId = form.CategoryId,
                        Name = form.Name,
                        Description = form.Description
                    };
                    var response = _subcategoryRepository.UpdateSubcategory(categoryForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Alt Kategori başarıyla güncellendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Alt Kategori güncellenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/alt-kategori-duzenle?id=" + form.Id);
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}