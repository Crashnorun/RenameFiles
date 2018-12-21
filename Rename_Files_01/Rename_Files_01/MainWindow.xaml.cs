using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;
using System.IO.Compression;


/*TODO:
 * Need to add the ability to create a zip file with the newly created files
 * Need to add the ability to skip a file
 */

namespace Rename_Files_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region ----PROPERTIES----
        // starting directory path
        public string folderPath = @"C:\Users\cportelli\Documents\Personal\City Tech\04 Student Submissions";
        public string folderName;
        int count = 0;

        // list of files in the folder
        public List<FileInfo> files;

        // list of new file names
        public List<string> newFiles = new List<string>();

        // list of student names
        public List<string> studentNames;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            PopulateStudenNames();                          // populate student names in list
            this.combo_Names.ItemsSource = studentNames;    // populate combobox
        }

        private void btn_SelFolder_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                // open to the last used folder
                if (folderPath != null)
                    dialog.SelectedPath = folderPath;

                dialog.ShowDialog();
                folderPath = dialog.SelectedPath;
                this.txt_FolderPath.Text = folderPath;
            }
        }

        private void txt_FolderPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            // reset list of new file names;
            newFiles = new List<string>();

            if (folderPath != null)
            {
                // get list of files in folder
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                files = new List<FileInfo>();
                files = dir.GetFiles().ToList();

                // get the last folder name in the directory
                // this is also the assignment name
                folderName = System.IO.Path.GetFileName(folderPath);
                if (folderName != null)
                    this.txt_Assignment.Text = folderName;

                count = 0;
                this.btn_Next.Content = "Next: 0 / " + files.Count;
                this.txt_Current.Text = files[count].Name;

                ActivateButton();
            }
        }

        private string NewFileName()
        {
            // get the file extension
            string fileExt = System.IO.Path.GetExtension(this.txt_Current.Text);

            // get the student name
            string studentName;
            if (this.combo_Names.SelectedIndex > 0)
                studentName = studentNames[this.combo_Names.SelectedIndex];
            else
                studentName = this.txt_StudentName.Text;

            // format new file name
            string newName = string.Format("{0}_{1}_{2}_{3}_{4}{5}",
                this.txt_Course.Text, this.txt_Initials.Text,
                this.txt_Semester.Text, this.txt_Assignment.Text,
                studentName, fileExt);

            return newName;
        }

        private void ActivateButton()
        {
            // if all the inputs are satisfied, activate the next button
            if (this.txt_Assignment != null && !string.IsNullOrEmpty(this.txt_Assignment.Text) &&
                this.txt_Course != null && !string.IsNullOrEmpty(this.txt_Course.Text) &&
                this.txt_FolderPath != null && !string.IsNullOrEmpty(txt_FolderPath.Text) &&
                this.txt_Initials != null && !string.IsNullOrEmpty(this.txt_Initials.Text) &&
                this.txt_Semester != null && !string.IsNullOrEmpty(this.txt_Semester.Text))
            {

                // determins where the student name is taken from
                if ((this.combo_Names.SelectedIndex == 0 && this.txt_StudentName != null && !string.IsNullOrEmpty(this.txt_StudentName.Text)) ||
                    this.combo_Names.SelectedIndex > 0)
                {
                    this.btn_Next.IsEnabled = true;
                    this.txt_New.Text = NewFileName();
                }
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            // save file
            if (cbx_Copy.IsChecked.Value)
            {
                File.Copy(files[count].FullName, Path.Combine(folderPath, NewFileName()));
            }
            else { }

            count += 1;
            // update the button text
            this.btn_Next.Content = "Next: " + count + " / " + files.Count;

            if (count < files.Count)
            {
                this.txt_Current.Text = files[count].Name;     // get the next file to update
                this.txt_New.Text = NewFileName();             // create the new file name
            }
            else
            {
                this.btn_Next.IsEnabled = false;

                //create zip file with all new files
                if (cbx_Zip.IsChecked.Value)
                {
                    //need zip file name
                    string zipFileName = string.Format("{0}_{1}_{2}_{3}",
                         this.txt_Course.Text, this.txt_Initials.Text,
                    this.txt_Semester.Text, this.txt_Assignment.Text);

                    // ZipFile.CreateFromDirectory(folderPath, folderPath + @"\" + zipFileName, CompressionLevel.Optimal, false);

                    //using (ZipArchive newFile = ZipFile.CreateFromDirectory(folderPath,folderPath + @"\" + zipFileName, CompressionLevel.Optimal, false))
                    //{

                    //}


                    //using(MemoryStream zipMS = new MemoryStream())
                    //{
                    // ZipFile.c   
                    //    using (ZipFile zipFile = new ZipFile())
                    //    {

                    //    }
                    //}

                }
            }
        }

        public void PopulateStudenNames()
        {
            studentNames = new List<string>();
            studentNames.Add("");
            studentNames.Add("Abdou_Mahamadou");
            studentNames.Add("Acevedo_Anthony");
            studentNames.Add("Aguayza_Wilson");
            studentNames.Add("Bhatti_Aqib");
            studentNames.Add("Cruz_Ivan");
            studentNames.Add("Dufort_Jewie");
            studentNames.Add("Ferguson_Avery");
            studentNames.Add("Garcia_Joshua");
            studentNames.Add("Mavlyutova_Albina");
            studentNames.Add("Niasse_Khady");
            studentNames.Add("Ponari_Xhulja");
            studentNames.Add("Royes_Brandon");
            studentNames.Add("Saeteros_Alex");
            studentNames.Add("Singh_Ryan");
        }

        private void combo_Names_DropDownClosed(object sender, EventArgs e)
        {
            if (this.combo_Names.SelectedIndex != 0)
            {
                this.txt_StudentName.IsEnabled = false;
                this.txt_StudentName.Text = string.Empty;
                ActivateButton();
            }
            else { this.txt_StudentName.IsEnabled = true; }
        }


        #region ----MISC EVENTS----

        private void txt_Course_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivateButton();
        }

        private void txt_Initials_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivateButton();
        }

        private void txt_Semester_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivateButton();
        }

        private void txt_Assignment_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivateButton();
        }

        private void txt_StudentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivateButton();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
