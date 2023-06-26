using SkladWPF.AdoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladWPF.AdoModel
{
    public static class AppDataConnect
    {
        public static string Var1 = "Data Source = DESKTOP-H7G4A3S\\SQLEXPRESS;Initial Catalog=SkladDbActual; Integrated Security=True"; // Подключение к базе данных
        public static SkladDbActualEntities6 bd = new SkladDbActualEntities6(); //Экзаемпляр подключения к модели базы данных ADO
        public static int IDUse; // Пользователь составления заявки
    }
}
