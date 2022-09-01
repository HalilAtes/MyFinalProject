using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Data Transformation Object
            //ProductTest();
            //CategoryTest();
            ProductTest2();

        }

        private static void ProductTest2()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),
                new CategoryManager(new EfCategoryDal()));

            var result1 = productManager.GetProductDetails();

            if (result1.Success == true)    
            {

                foreach (var product in result1.Data)
                {
                    Console.WriteLine(product.ProductName + "      /             " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result1.Message);
            }

            foreach (var product in result1.Data)
            {
                Console.WriteLine(product.ProductName + "      /               " + product.CategoryName);

            }
        }



        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),
                  new CategoryManager(new EfCategoryDal()));

            foreach (var product in productManager.GetByUnitPrice(60, 100).Data)
            {
                Console.WriteLine(product.ProductName);

            }
        }




    }
}
