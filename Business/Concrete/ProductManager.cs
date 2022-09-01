using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation; //
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;                               // İnterface tipinde global bir değişken tanımlandı..
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)           // Bir manager kendisinden başka bir Dal'ı enjekte edemez.
        {  
            _productDal = productDal;
            _categoryService = categoryService; 

             
        }

        [SecuredOperation("product.add")]   // Korunan Operasyon ve Claimler 
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]



        public IResult Add(Product product)
        {
            //Business codes
            // Ayrı ayrı yapılır.
            // Validation = Doğrulama İlgili şeyin yapısı hakkında 
            
            //Eğer mevcut kategori sayısı 15 'i geçityse sisteme yeni ürün eklenemez.

            IResult result = BusinessRules.Run(
                CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCatogoryLimitExceded());

            if (result!=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);









            /*  
             *  
             *   
             *   
            var context = new ValidationContext<Product>(product);  // Doğrulama 
            ProductValidator productValidator = new ProductValidator();
            var result = productValidator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            
             if (product.UnitPrice <= 0)
             {
                 //return new ErrorResult(Messages.UnitPriceInvalid);
             }

             if (product.ProductName.Length < 2)
             {
                 // magic strings
                 return new ErrorResult(Messages.ProductNameInvalid);
             }
            */


        }
        [SecuredOperation("product.add")]
        [CacheAspect] // key,value
        public IDataResult<List<Product>> GetAll()
        {
            //Business codes
            if (DateTime.Now.Hour == 9)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);    // Bakım zamanı  
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);



            //InMemoryProductDal inMemoryProductDal = new InMemoryProductDal(); 
            // Kural : Bir iş sınıfı başka sınıfları newlemez..

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));       // Gönderilen CategoryId ye göre filtrele.
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)            // Bu metodun sadece bu class'ın içersinde kullanmak istiyorum..
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;     // Arkaplanda bir sql sorgusu üretip ona göre ürünler getirir..
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();       // Any var mı ?
            if(result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCatogoryLimitExceded()
        {
            var result = _categoryService.GetAll();     
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
