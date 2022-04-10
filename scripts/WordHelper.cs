using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using PR_TRPO.Forms;
using PR_TRPO.Scripts;
using MySql.Data.MySqlClient;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;

namespace PR_TRPO.Scripts
{
    class WordHelper
    {
        private FileInfo _fileInfo;
        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("Шаблон отчёта не найден. Сформировать отчёт невозможно. Обратитесь к администратору.");
            }
        }
        internal bool Proccess(Dictionary<string, string> items, string path)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(file);
                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(
                    FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                }
                Object newFileName = path;
                app.ActiveDocument.SaveAs2(newFileName);
                MessageBox.Show("Отчёт сформирован!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                app.Visible = true;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
