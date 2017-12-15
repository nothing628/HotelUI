using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIHotel.App.View
{
    public class ViewTemplateManager : ITemplateManager
    {
        private Dictionary<ITemplateKey, ITemplateSource> dictTmp = new Dictionary<ITemplateKey, ITemplateSource>();
        private ViewCompiler compiler;

        public ViewTemplateManager(ViewCompiler compiler)
        {
            this.compiler = compiler;
        }

        public void AddDynamic(ITemplateKey key, ITemplateSource source)
        {
            dictTmp.Add(key, source);
        }

        public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
        {
            return new NameOnlyTemplateKey(name, resolveType, context);
        }

        public ITemplateSource Resolve(ITemplateKey key)
        {
            if (dictTmp.ContainsKey(key))
                return dictTmp[key];

            var viewPath = compiler.ResolvePath(key.Name);

            if (!compiler.IsExists(key.Name)) throw new ViewNotFoundException(key.Name, viewPath);

            var viewContent = compiler.GetFileContent(viewPath);
            var result = new LoadedTemplateSource(viewContent, viewPath);

            dictTmp.Add(key, result);

            return Resolve(key);
        }
    }
}
