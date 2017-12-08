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

namespace UIHotel
{
    public class AppHtmlRenderer
    {
        // Map from file to template Key
        private Dictionary<string, string> TemplateKeyList = new Dictionary<string, string>();
        private TemplateServiceConfiguration config;
        private IRazorEngineService service;

        public AppHtmlRenderer()
        {
            InitRazorEngine();
        }

        private void InitRazorEngine()
        {
            config = new TemplateServiceConfiguration();
            // .. configure your instance
            config.BaseTemplateType = typeof(TemplateBase<>);
            config.Language = Language.CSharp; // VB.NET as template language.
            config.EncodedStringFactory = new RawStringFactory(); // Raw string encoding.
            config.EncodedStringFactory = new HtmlEncodedStringFactory(); // Html encoding.
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(x => { });

            service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }

        public string RegisterTemplate(string filename, string templateName = "")
        {
            var templateKey = Guid.NewGuid().ToString();
            var fileContents = "";

            if (!File.Exists(filename))
                throw new Exception("File Not Found!");

            using (var fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                fileContents = reader.ReadToEnd();
            }

            var templateSource = new LoadedTemplateSource(fileContents, filename);
            
            if (templateName != "")
                Engine.Razor.AddTemplate(templateName, templateSource);

            Engine.Razor.Compile(templateSource, templateKey, null);

            TemplateKeyList.Add(templateKey, filename);

            return templateKey;
        }

        public string Render(string filename)
        {
            string templateKey = GetTemplateKey(filename);

            if (templateKey == "")
                return "";

            try
            {
                return Engine.Razor.RunCompile(templateKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Render(string filename, DynamicViewBag viewBag)
        {
            string templateKey = GetTemplateKey(filename);

            if (templateKey == "")
                return "";

            try
            {
                return Engine.Razor.RunCompile(templateKey, viewBag: viewBag);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Render<T>(string filename, T model)
        {
            string templateKey = GetTemplateKey(filename);

            if (templateKey == "")
                return "";

            try
            {
                return Engine.Razor.RunCompile(templateKey, model: model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetTemplateKey(string filename)
        {
            string result = (from a in TemplateKeyList
                             where a.Value == filename
                             select a.Key).FirstOrDefault();

            return result;
        }
    }
}
