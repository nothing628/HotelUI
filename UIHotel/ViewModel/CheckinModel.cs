using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIHotel.ViewModel
{
    public class CheckinModel
    {
        public string retStr;
        public string Return()
        {
            var oThread = new Thread(() => {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var dialog = new OpenFileDialog();
                dialog.Filter = "Document|*.pdf;*.jpg;*.jpeg;*.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileInfo(dialog.FileName);
                    var ext = file.Extension;
                    var newName = Guid.NewGuid() + ext;
                    var newPath = Path.Combine(basePath, @"Upload\", newName);

                    file.CopyTo(newPath);

                    this.retStr = newName;
                }
                else
                {
                    this.retStr = null;
                }
            });
            
            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
            oThread.Join();

            return this.retStr;
        }
    }
}
