using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using HelloAutocadPlugIn.View;
using HelloAutocadPlugIn.ViewModel;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Exception = System.Exception;

namespace HelloAutocadPlugIn.Model
{
    /// <summary>
    /// Класс комманд для командной строки AutoCAD
    /// </summary>
    public class AutoCadCommands : IExtensionApplication
    {
        /// <summary>
        /// Функция инициализации (выполняется при загрузке плагина)
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// Функция, выполняемая при выгрузке плагина
        /// </summary>
        public void Terminate()
        {
        }

        /// <summary>
        /// Команда вызова плагина
        /// </summary>
        [CommandMethod("ChangeProperties", CommandFlags.Modal)]
        public void ChangeProperties()
        {
            // Получаем текущий документ, его БД и Editor
            Document acDocument = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDocument.Database;
            Editor acEditor = acDocument.Editor;

            // Начинаем транзакцию
            Transaction transaction = acCurDb.TransactionManager.StartTransaction();

#if !DEBUG
            try
            {
#endif
                // Создаём окно плагина
                MainView mainWindow = new MainView();

                //Создаём модель данных 
                DataService dataService = new DataService(transaction, acCurDb, acEditor);
            
                //Создаём viewmodel и соединяем элементы под MVVM
                MainViewModel mainViewModel = new MainViewModel(dataService);
                mainWindow.DataContext = mainViewModel;

                //Вызов окна плагина в режиме диалога
                Application.ShowModalWindow(mainWindow);
#if !DEBUG
            }
            catch (Exception exception)
            {
                acEditor.WriteMessage("Error: " + exception.Message);
            }
#endif

            // Освобождение транзакции, если у транзакции не было Commit(), то изменения не применяются
            transaction.Dispose();
        }
    }
}
