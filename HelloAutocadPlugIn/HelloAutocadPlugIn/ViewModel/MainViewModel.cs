using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using HelloAutocadPlugIn.Messages;
using HelloAutocadPlugIn.Model;
using HelloAutocadPlugIn.ViewModel.BindHelpers;

namespace HelloAutocadPlugIn.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region - private fields -

        /// <summary>
        /// Текущие данные Autocad
        /// </summary>
        private readonly DataService _dataService;

        #endregion - private fields -

        #region - Changeable properties -

        private ObservableCollection<LayerTableRecord> _layers;
        /// <summary>
        /// Слои в текущем документе
        /// </summary>
        public ObservableCollection<LayerTableRecord> Layers
        {
            get
            {
                return _layers;
            }

            set
            {
                if (_layers == value)
                {
                    return;
                }
                _layers = value;
                if (_layers[0] != null)
                    SelectedLayer = _layers[0];

                RaisePropertyChanged();
            }
        }

        private LayerTableRecord _selectedLayer;
        /// <summary>
        /// Выбранный слой
        /// </summary>
        public LayerTableRecord SelectedLayer
        {
            get
            {
                return _selectedLayer;
            }

            set
            {
                if (_selectedLayer == value)
                {
                    return;
                }

                _selectedLayer = value;
                GetEntities();
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<ChangeableLine> _findedLines;
        /// <summary>
        /// Список найденных объектов на слое - типа линия
        /// </summary>
        public ObservableCollection<ChangeableLine> FindedLines
        {
            get
            {
                return _findedLines;
            }

            set
            {
                if (_findedLines == value)
                {
                    return;
                }
                _findedLines = value;

                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ChangeableDBPoint> _findedPoints;
        /// <summary>
        /// Список найденных объектов на слое - типа точка
        /// </summary>
        public ObservableCollection<ChangeableDBPoint> FindedPoints
        {
            get
            {
                return _findedPoints;
            }

            set
            {
                if (_findedPoints == value)
                {
                    return;
                }
                _findedPoints = value;
                
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ChangeableCircle> _findedCircles;
        /// <summary>
        /// Список найденных объектов на слое - типа окружность
        /// </summary>
        public ObservableCollection<ChangeableCircle> FindedCircles
        {
            get
            {
                return _findedCircles;
            }

            set
            {
                if (_findedCircles == value)
                {
                    return;
                }
                _findedCircles = value;

                RaisePropertyChanged();
            }
        }

        #endregion - Changeable properties -

        #region - Commands -

        /// <summary>
        /// Команда при нажатии на кнопку Ok
        /// </summary>
        public RelayCommand OkCommand { get; private set; }

        /// <summary>
        /// Команда при нажатии на кнопку Cancel
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        #endregion - Commands -

        #region - Constructors -

        public MainViewModel(DataService dataService)
        {
            _dataService = dataService;
            Layers = _dataService.GetLayers();
            // Инициализация комманд
            OkCommand = new RelayCommand(OkCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        #endregion - Constructors -

        #region - Executable methods for commands -

        /// <summary>
        /// Сохранить изменения и закрыть форму
        /// </summary>
        private void OkCommandExecute()
        {
            _dataService.CommitChanges();
            Messenger.Default.Send(new CloseMainViewMessage());
        }

        /// <summary>
        /// Закрыть форму (изменения не сохраняются)
        /// </summary>
        private void CancelCommandExecute()
        {
            Messenger.Default.Send(new CloseMainViewMessage());
        }

        #endregion - Executable methods for commands -

        #region - private methods -

        /// <summary>
        /// Получить объекты для текущего слоя
        /// </summary>
        private void GetEntities()
        {
            if (SelectedLayer == null) return;

            //Получение Линий
            var lines = _dataService.GetEntities(SelectedLayer, typeof(Line)).Cast<Line>();

            var changebleLines = new ObservableCollection<ChangeableLine>();

            foreach (var line in lines)
            {
                // Обёртывание в вспомогательный класс
                changebleLines.Add(new ChangeableLine(line));
            }
            FindedLines = changebleLines;


            //Получение Точек
            var points = _dataService.GetEntities(SelectedLayer, typeof(DBPoint)).Cast<DBPoint>();

            var changeblePoints = new ObservableCollection<ChangeableDBPoint>();
            foreach (var point in points)
            {
                // Обёртывание в вспомогательный класс
                changeblePoints.Add(new ChangeableDBPoint(point));
            }
            FindedPoints = changeblePoints;


            //Получение Окружностей
            var circles = _dataService.GetEntities(SelectedLayer, typeof(Circle)).Cast<Circle>();

            var changebleCircles = new ObservableCollection<ChangeableCircle>();
            foreach (var circle in circles)
            {
                // Обёртывание в вспомогательный класс
                changebleCircles.Add(new ChangeableCircle(circle));
            }
            FindedCircles = changebleCircles;
        }

        #endregion - private methods -

        #region - INotifyPropertyChanged realization  -

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "") // Волшебство .NET 4.5
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion - INotifyPropertyChanged realization  -
    }
}
