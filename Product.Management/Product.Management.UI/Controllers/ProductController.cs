using Product.Management.Business.Repository.Abstract;
using Product.Management.Data.Models;
using Product.Management.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Product.Management.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //[OutputCache(CacheProfile = "OneMinute")]
        [Route("urunler")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Index(string catId="0",string subId="0")
        {
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                ViewBag.CategoryId = catId;
                ViewBag.SubcategoryId = subId;
                ProductViewModel model = new ProductViewModel
                {
                    Categories = _productRepository.GetSelectListCategories(),
                    Subcategories = _productRepository.GetSelectListSubcategories(int.Parse(catId))

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

                Tuple<IEnumerable<Products>, int> dataPagingList = _productRepository.GetProductList(int.Parse(catId), int.Parse(subId),start, length, orderby, search);

                totalRecords = dataPagingList.Item2;  
                return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = dataPagingList.Item1 });
            }
        }
        [Route("urun-ekle")]
        public ActionResult Add()
        {
            ViewBag.Response = TempData["ResponseMessage"];
            ProductViewModel model = new ProductViewModel
            {
                Categories = _productRepository.GetSelectListCategories()
            };
            return View(model);
        }
        [Route("urun-duzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.Response = TempData["ResponseMessage"];
            var detailProduct = _productRepository.DetailProduct(id);
            ProductViewModel model = new ProductViewModel
            {
                Product = detailProduct,
                Categories = _productRepository.GetSelectListCategories(),
                Subcategories=_productRepository.GetSelectListSubcategories(detailProduct.CategoryId)
            };
            return View(model);
        }
        [AcceptVerbs("POST")]
        public ActionResult ProductFormAdd(ProductFormAddViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Products productForm = new Products
                    {
                        CategoryId = form.CategoryId,
                        SubcategoryId = form.SubcategoryId,
                        Name = form.Name,
                        Price = form.Price,
                        KdvRate = form.KdvRate

                    };
                    var response = _productRepository.InsertProduct(productForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Ürün başarıyla eklendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Ürün eklenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/urun-ekle");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [AcceptVerbs("POST")]
        public ActionResult ProductFormEdit(ProductFormEditViewModel form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Products categoryForm = new Products
                    {
                        Id = form.Id,
                        CategoryId = form.CategoryId,
                        SubcategoryId = form.SubcategoryId,
                        Name = form.Name,
                        Price = form.Price,
                        KdvRate = form.KdvRate
                    };
                    var response = _productRepository.UpdateProduct(categoryForm);

                    if (response)
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-success' role='alert'>Ürün başarıyla güncellendi.</div> ";
                    }
                    else
                    {
                        TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Ürün güncellenirken bir hata oluştu!</div> ";
                    }

                }
                else
                {
                    TempData["ResponseMessage"] = "<div style='text-align:center' class='alert alert-danger' role='alert'>Lütfen zorunlu alanları doldurunuz.</div> ";
                }

                return Redirect("/urun-duzenle?id=" + form.Id);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        
        public JsonResult GetSubcategories(int id)
        {
            return Json(_productRepository.GetSelectListSubcategories(id), JsonRequestBehavior.AllowGet);
        }
    }
}