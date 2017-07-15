using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Web.Razor;
using Microsoft.CSharp;

namespace TemplateEngine
{
    public class TemplateGenerator
    {
        public CustomTemplateBase Generate(string defaultClassName, string defaultNamespace,
            TextReader reader, IEnumerable<string> additionalDlls)
        {
            var defaultBaseClass = "TemplateEngine.CustomTemplateBase";

            //CREATE THE HOST
            var language = new CSharpRazorCodeLanguage();
            var host = new RazorEngineHost(language)
            {
                DefaultBaseClass = defaultBaseClass,
                DefaultClassName = defaultClassName,
                DefaultNamespace = defaultNamespace,
            };

            host.NamespaceImports.Add("System");
            host.NamespaceImports.Add("System.Collections");
            host.NamespaceImports.Add("System.Collections.Generic");
            host.NamespaceImports.Add("System.Dynamic");
            host.NamespaceImports.Add("System.Linq");

            //CREATE THE ENGINE
            var engine = new RazorTemplateEngine(host);

            //Generate the code
            var razorResult = engine.GenerateCode(reader);
            
            var compileParameters = new CompilerParameters();

            var dlls = new List<string> 
            {
                "mscorlib.dll",
                "system.dll",
                "system.core.dll",
                "microsoft.csharp.dll",
                "TemplateEngine.dll"
            };
            dlls.AddRange(additionalDlls);

            foreach (var reference in dlls)
            {
                compileParameters.ReferencedAssemblies.Add(reference);
            }
            
            //Compile it
            var compilerResults =
                new CSharpCodeProvider()
                    .CompileAssemblyFromDom(
                        compileParameters,
                        razorResult.GeneratedCode
                    );

            foreach (var error in compilerResults.Errors)
            {
                Console.WriteLine("Error: {0}", error);
            }

            var template = (CustomTemplateBase) compilerResults.CompiledAssembly
                .CreateInstance(string.Format("{0}.{1}", defaultNamespace, defaultClassName));
            if (template == null)
            {
                throw new InvalidOperationException("Cannot generate template");
            }
            return template;
        }
    }
}