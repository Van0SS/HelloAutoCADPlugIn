using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using HelloAutocadPlugIn.Enums;

namespace HelloAutocadPlugIn.Model
{
    /// <summary>
    /// Связь с данными AutoCAD
    /// </summary>
    public class DataService
    {
        /// <summary>
        /// БД текущего документа
        /// </summary>
        private Database _acCurDatabase;

        /// <summary>
        /// Текущая открытая транзакция
        /// </summary>
        private Transaction _acCurTransaction;

        /// <summary>
        /// Editor текущего документа
        /// </summary>
        private Editor _acCurEditor;

        /// <summary>
        /// Соответствия типа класса и строки для поиска элементов
        /// </summary>
        public Dictionary<Type, string> ObjectTypes { get; private set; } =
            new Dictionary<Type, string>
            {
                {typeof(Line), "Line"},
                {typeof(DBPoint), "Point"},
                {typeof(Circle), "Circle"}
            };

        public DataService(Transaction transaction, Database database, Editor editor)
        {
            _acCurDatabase = database;
            _acCurTransaction = transaction;
            _acCurEditor = editor;
        }

        /// <summary>
        /// Получить слои текущего документа
        /// </summary>
        public ObservableCollection<LayerTableRecord> GetLayers()
        {
            var layers = new ObservableCollection<LayerTableRecord>();
            // открываем таблицу слоев документа
            LayerTable acLayerTable =
                _acCurTransaction.GetObject(_acCurDatabase.LayerTableId, OpenMode.ForWrite) as LayerTable;

            foreach (ObjectId tableId in acLayerTable)
            {
                //Получить слой, с возможностью изменять даже замороженный слой.
                var layer = (LayerTableRecord)_acCurTransaction.GetObject(tableId, OpenMode.ForWrite, false, true);
                layers.Add(layer);
            }
            return layers;
        }

        /// <summary>
        /// Получить объекты указанного типа и слоя
        /// </summary>
        /// <param name="layer">Слой</param>
        /// <param name="type">Тип объектов</param>
        public ObservableCollection<Entity> GetEntities(LayerTableRecord layer, Type type)
        {
            // Создаем переменную, в которой будут содержаться данные для фильтра
            TypedValue[] filterlist = new TypedValue[2];

            // Первый аргумент указывает, что мы задаем тип фильтрации - по объектам
            // Второй аргумент тип объекта
            filterlist[0] = new TypedValue((int)AdTypeCodes.Entity, ObjectTypes[type]);

            // Фильтрация по слою
            filterlist[1] = new TypedValue((int)AdTypeCodes.Layer, layer.Name);

            SelectionFilter filter = new SelectionFilter(filterlist);

            // Поиск
            PromptSelectionResult selRes = _acCurEditor.SelectAll(filter);

            // Если ничего не найдено, то статус Error, реальные ошибки не обработать :(
            if (selRes.Status != PromptStatus.OK)
            {
                // Если ничего не найдено, возвращаем пустое множество
                return new ObservableCollection<Entity>();
            }

            ObjectId[] ids = selRes.Value.GetObjectIds();

            var entities = new ObservableCollection<Entity>();

            // Проходим по всем полученным объектам
            foreach (ObjectId id in ids)
            {
                // Получаем доступ на редактирование (даже заблокированных) и приводим к интерфейсу Entity
                Entity entity = (Entity)_acCurTransaction.GetObject(id, OpenMode.ForWrite, false, true);
                entities.Add(entity);
            }
            return entities;
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void CommitChanges()
        {
            _acCurTransaction.Commit();
        }



    }
}
