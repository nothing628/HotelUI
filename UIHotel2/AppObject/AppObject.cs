﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;
using Chromium.Remote;
using Chromium.Remote.Event;
using Chromium.WebBrowser;

namespace UIHotel2.AppObject
{
    class AppObject: BaseObject
    {
        public string retStr;
        public string originalName;

        public override string ObjectName => "App";

        public override void Register(JSObject obj)
        {
            base.Register(obj);
            Self.AddFunction("OpenDialog").Execute += OpenDialogExecute;
        }

        private void OpenDialogExecute(object sender, CfrV8HandlerExecuteEventArgs e)
        {
            var is_callback = e.Arguments[0].IsFunction;

            if (is_callback)
            {
                //no-filter
                var callback = e.Arguments[0];

                OpenDialog(callback);
            } else
            {
                //with-filter
                var filter = e.Arguments[0].StringValue;
                var callback = e.Arguments[1];

                OpenDialog(callback, filter);
            }
        }

        public void OpenDialog(CfrV8Value callback)
        {
            OpenDialog(callback, "Document|*.pdf;*.jpg;*.jpeg;*.png");
        }

        public void OpenDialog(CfrV8Value callback, string Filter)
        {
            var oThread = new Thread(() => {
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var dialog = new OpenFileDialog();
                dialog.Filter = Filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var file = new FileInfo(dialog.FileName);
                    var ext = file.Extension;
                    var newName = Guid.NewGuid() + ext;
                    var newPath = Path.Combine(basePath, @"Upload\", newName);

                    file.CopyTo(newPath);

                    this.originalName = file.Name;
                    this.retStr = newName;
                }
                else
                {
                    this.originalName = null;
                    this.retStr = null;
                }
            });

            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
            var isSafe = oThread.Join(new TimeSpan(2, 0, 0));
            var callbackArgs = CfrV8Value.CreateObject(new CfrV8Accessor());
            if (isSafe)
                oThread.Abort();

            callbackArgs.SetValue("hashname", CfrV8Value.CreateString(retStr), CfxV8PropertyAttribute.ReadOnly);
            callbackArgs.SetValue("filename", CfrV8Value.CreateString(originalName), CfxV8PropertyAttribute.ReadOnly);

            callback.ExecuteFunction(null, new CfrV8Value[] { callbackArgs });
        }
    }
}
