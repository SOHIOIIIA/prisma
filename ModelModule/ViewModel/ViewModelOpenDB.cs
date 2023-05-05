using Aspose.Cells;
using ExcelDataReader;
using Microsoft.Win32;
using ModelModule.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Regions;
using Prisma.Core.Abstractions;
using Prism.Mvvm;

namespace ModelModule.ViewModel
{
    public class ViewModelOpenDB
    {
        private readonly IRegionManager _regionManager;

        private DataTable _databases = new DataTable();
        public DataTable Databases
        {
            get { return _databases; }
            set
            {
                if (_databases != value)
                {
                    SetProperty(ref _databases, value);
                }
            }
        }

        public ViewModelOpenDB(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }

        private int _rowCount;
        private int _columnCount;

        private string _filePath = "";

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    SetProperty(ref _filePath, value);
                }
            }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                if (_rowCount != value)
                {
                    SetProperty(ref _rowCount, value);
                }
            }
        }


        public int ColumnCount
        {
            get { return _columnCount; }
            set
            {
                if (_columnCount != value)
                {
                    SetProperty(ref _columnCount, value);
                }
            }
        }


        private DataTable _excelData;
        IExcelDataReader edr;



        private RelayCommand openExcel;
        public ICommand OpenExcel => openExcel ??= new RelayCommand(PerformOpenExcel);

        private void PerformOpenExcel()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXCEL Files (*.xlsx)|*.xlsx|EXCEL Files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
                return;
            FilePath = openFileDialog.FileName;
            var model = ProjectModel.Instance;
            model.DataBasepath = FilePath;
            var task = Task.Run(() => ReadExcel());
            //ReadExcelData(filename);
            //Workbook workbook = new Workbook(FilePath);
            //Worksheet worksheet = workbook.Worksheets[0];
            // Получить количество строк и столбцов
            //RowCount = worksheet.Cells.MaxDataRow;
            //ColumnCount = worksheet.Cells.MaxDataColumn;
            //DataTable = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
            //dataGrid.ItemsSource = dataTable.DefaultView;


        }

        public void ReadExcel()
        {
            Workbook workbook = new Workbook(FilePath);
            Worksheet worksheet = workbook.Worksheets[0];
            // Получить количество строк и столбцов
            RowCount = worksheet.Cells.MaxDataRow;
            ColumnCount = worksheet.Cells.MaxDataColumn;
            Databases = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true); // Записал на прямую и сделал попытку ленивой загрузки
            //Databases = dataTable; // DataTable dataTable
        }
    }
}
