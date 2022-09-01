using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]     // İsteği yaparken nasıl ulaşsın
    [ApiController]                 // ATTRIBUTE = Class ile bilgi verme imzalama
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention 
        //Ioc  Container -- Inversion of control
        IProductService _productService;        

        public ProductsController(IProductService productService  )
        {
            _productService = productService;                       // Gelenin referansı _productService'e atanmış oldu.
        }

        [Authorize(Roles = "product.list1")]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Dependency chain -- 
             
            var result = _productService.GetAll();
            if (result.Success )
            {
                return Ok(result);      //Ok 200
            }
            return BadRequest(result); 
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("add")]       // add - update - delete
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




    }
}
