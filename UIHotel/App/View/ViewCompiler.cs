using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.View
{
    public class ViewCompiler
    {
        private TemplateServiceConfiguration viewConfig;
        private IRazorEngineService service;
        private Dictionary<string, string> tempKey = new Dictionary<string, string>();
        public string ViewDir { get; set; } = @"View";
        public string ViewPath { get => Path.Combine(AppMain.Main.BaseDir, ViewDir); }
        public const string ViewExt = ".cshtml";

        public ViewCompiler()
        {
            InitRazorEngine();
        }

        private void InitRazorEngine()
        {
            viewConfig = new TemplateServiceConfiguration();
            // .. configure your instance
            viewConfig.BaseTemplateType = typeof(TemplateBase<>);
            viewConfig.Language = Language.CSharp; // VB.NET as template language.
            viewConfig.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            viewConfig.EncodedStringFactory = new HtmlEncodedStringFactory(); // Html encoding.
            viewConfig.DisableTempFileLocking = true;
            viewConfig.CachingProvider = new DefaultCachingProvider(x => { });
            viewConfig.TemplateManager = new ViewTemplateManager(this);

            service = RazorEngineService.Create(viewConfig);

            Engine.Razor = service;
        }

        public string ResolvePath(string ViewName)
        {
            string path = ViewName.Replace('.', Path.DirectorySeparatorChar);
            string file = path + ViewExt;

            return Path.Combine(ViewPath, file);
        }

        public bool IsExists(string ViewName)
        {
            return File.Exists(ResolvePath(ViewName));
        }

        public string GetFileContent(string viewPath)
        {
            string fileContents;

            using (var fileStream = File.Open(viewPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                fileContents = reader.ReadToEnd();
            }

            return fileContents;
        }

        public void RegisterTemplate(string viewName, string viewAlias, Type modelType = null)
        {
            string viewPath = ResolvePath(viewName);

            if (!IsExists(viewName)) throw new ViewNotFoundException(viewName, viewPath);

            var viewContent = GetFileContent(viewPath);
            var templateSource = new LoadedTemplateSource(viewContent, viewPath);

            Engine.Razor.Compile(templateSource, viewAlias, modelType);
        }

        public string Render(string ViewName)
        {
            return Engine.Razor.RunCompile(ViewName);
        }

        public string Render(string ViewName, object data, DynamicViewBag viewBag = null)
        {
            return Engine.Razor.RunCompile(ViewName, null, data, viewBag);
        }
    }
}
