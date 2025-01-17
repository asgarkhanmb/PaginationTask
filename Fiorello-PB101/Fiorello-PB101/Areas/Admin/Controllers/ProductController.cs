﻿using Fiorello_PB101.Helpers;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 3)
        {
            var produtcs = await _productService.GetAllPaginateAsync(page, take);

            var mappedDatas = _productService.GetMappedDatas(produtcs);

            int totalPage = await GetPageCountAsync(3);

            Paginate<ProductVM> paginateDatas = new(mappedDatas, totalPage, page);


            return View(paginateDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);

        }
    }
}
