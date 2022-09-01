using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersizdir.";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string ProductCountOfCategoryError="Kategorinin ürün limitini aştınız!";
        public static string ProductNameAlreadyExists = "Aynı isimde ürün bulunmaktadır";
        public static string CategoryLimitExceded="Kategori limiti aşıldı.";
        public static string AuthorizationDenied= "Yetkiniz bulunmamaktadır.";
        public static string UserRegistered="Kayıt olundu.";
        public static string UserNotFound="Kullanıcı bulunamadı.";
        public static string PasswordError="Şifreniz hatalıdır.";
        public static string SuccessfulLogin="Giriş başarılı!";
        public static string UserAlreadyExists="Kullanıcı bulunmaktadır.";
        public static string AccessTokenCreated="Erişim token'i oluşturuldu.";
        public static string ProductUpdated="Ürün güncellendi.";
    }
}
