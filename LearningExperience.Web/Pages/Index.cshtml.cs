using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Helper.Pages
{
    using System.IO;

    using LearningExperience.Core.Exceptions;
    using LearningExperience.Core.Extensions;
    using LearningExperience.Core.Interfaces;
    using LearningExperience.Core.Models;

    public class IndexModel : PageModel
    {
        public readonly DocumentsScheme DocumentsScheme;

        private readonly IDocumentsSchemeService documentsSchemeService;

        public IndexModel(IDocumentsSchemeService documentsSchemeService)
        {
            this.documentsSchemeService = documentsSchemeService;
            DocumentsScheme = documentsSchemeService.GetScheme();
        }

        public string HtmlPageContent { get; set; }

        public void OnGet(string pageName)
        {
            OpenClosePage(string.IsNullOrEmpty(pageName) ? "About" : pageName);
        }

        public void OnPost(string pageName)
        {
            OpenClosePage(pageName, false);
        }

        private void OpenClosePage(string pageName, bool openPage = true)
        {
            var document = DocumentsScheme.Documents.FirstOrDefaultFromMany(
                w => w.Documents,
                doc => doc.Name == pageName);
            if (document == null) throw new PageNotFoundException($"Not found document with name equal {pageName}");
            document.IsOpen = openPage;
            HtmlPageContent = !string.IsNullOrEmpty(document.Path)
                                  ? System.IO.File.ReadAllText(
                                      Path.Combine(documentsSchemeService.GetDocumentsSchemeFolder(), document.Path))
                                  : null;
            ViewData[nameof(DocumentsScheme)] = DocumentsScheme;
        }
    }
}
