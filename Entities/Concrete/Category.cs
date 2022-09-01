using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public  class Category :IEntity                 // Bir veritabanı nesnesi olduğunu IEntity ile belirtmiş olduk..
    {

        public int CategoryId { get; set; }
        public string  CategoryName { get; set; }
    }
}







/* 
 * Çıplak class kalmasınn           ÇCK   
1.CONCRETE klasörünün altındaki class'lar Veritabanı tablolarıdır..     SOMUT
2.ABSTRACT = İnterface - Abstract class - Base Classlar                 SOYUT
    Bağımlılıklar minimize edilir..

3.public = Diğer katmanlar da erişebilir..
4.Business = Ürünü kontrol edicek
5.Console = Ürünü göstericek
6.İnternal class = O projedeki herkes erişebilir..
7.İş yapan class'ların İnterface'ini oluştur..
8.İnterface'in operasyonları publictir..











*/
