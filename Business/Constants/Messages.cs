using Core.Entities.Concrete;
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
        public static string MaintenanceTime = "Sistem bakımda";
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kayıt bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists="Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        public static string WrongDate = "Wrong Date!";
        public static string WrongInput = "Wrong Input!";
        public static string ListEmpty = "List is null or empty!";
        public static string Conflict = "Entity already exist!";
        public static string Created = "Created successfully!";
        public static string InvalidAmount = "Invalid amount!";
        public static string OncePerDay = "You can only enter data once per day!";
    }
}
