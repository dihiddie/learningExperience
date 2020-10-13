namespace LearningExperience.Web.Pages
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using LearningExperience.Core.Exceptions;
    using LearningExperience.Core.Extensions;
    using LearningExperience.Core.Interfaces;
    using LearningExperience.Core.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly IDocumentsSchemeService documentsSchemeService;

        public IndexModel(IDocumentsSchemeService documentsSchemeService)
        {
            this.documentsSchemeService = documentsSchemeService;
            DocumentsScheme = documentsSchemeService.GetScheme();
        }

        public DocumentsScheme DocumentsScheme { get; set; }

        public string HtmlPageContent { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public void OnGet(string pageName)
        {
            OpenClosePage(string.IsNullOrEmpty(pageName) ? "Development" : pageName, pageName != null);
        }

        public void OnPost(string pageName)
        {
            OpenClosePage(pageName, false);
        }

        public void OnPostSearch()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                var doc = documentsSchemeService.GetRenewedScheme();
                DocumentsScheme.Documents = doc.Documents;
                ViewData[nameof(DocumentsScheme)] = DocumentsScheme;
                Title.SearchText = null;
                return;
            }

            var filteredList = new List<DocumentsScheme.Document>();

            for (int i = 0; i < DocumentsScheme.Documents.Count; i++)
            {
                var level1 = DocumentsScheme.Documents[i];
                if (level1.Value.Contains(SearchText)) filteredList.Add(level1);

                for (int j = 0; j < level1.Documents.Count; j++)
                {
                    var level2 = level1.Documents[j];
                    if (level2.Value.ToLower().Contains(SearchText))
                    {
                        if (!filteredList.Contains(level1))
                        {
                            level1.Documents = new List<DocumentsScheme.Document>();
                            filteredList.Add(level1);
                        }

                        level1.Documents.Add(level2);
                    }

                    for (int k = 0; k < level2.Documents.Count; k++)
                    {
                        var level3 = level2.Documents[k];
                        if (level3.Value.ToLower().Contains(SearchText))
                        {
                            if (!filteredList.Contains(level1))
                            {
                                level1.Documents = new List<DocumentsScheme.Document>();
                                filteredList.Add(level1);
                            }

                            if (!level1.Documents.Contains(level2))
                            {
                                level2.Documents = new List<DocumentsScheme.Document>();
                                level1.Documents.Add(level2);
                            }

                            level2.Documents.Add(level3);
                        }

                        var count = level3.Documents.Count;
                        for (int l = 0; l < count; l++)
                        {
                            var level4 = level3.Documents[l];
                            if (level4.Value.ToLower().Contains(SearchText))
                            {
                                if (!filteredList.Contains(level1))
                                {
                                    level1.Documents = new List<DocumentsScheme.Document>();
                                    filteredList.Add(level1);
                                }

                                if (!level1.Documents.Contains(level2))
                                {
                                    level2.Documents = new List<DocumentsScheme.Document>();
                                    level1.Documents.Add(level2);
                                }

                                if (!level2.Documents.Contains(level3))
                                {
                                    level3.Documents = new List<DocumentsScheme.Document>();
                                    level2.Documents.Add(level3);
                                }

                                level3.Documents.Add(level4);
                            }
                        }
                    }
                }
            }

            DocumentsScheme.Documents = filteredList;
            ViewData[nameof(DocumentsScheme)] = DocumentsScheme;
            Title.SearchText = SearchText;
        }

        private void OpenClosePage(string pageName, bool openPage = true)
        {
            var document = DocumentsScheme.Documents.FirstOrDefaultFromMany(
                w => w.Documents,
                doc => doc.Name == pageName);
            if (document == null) throw new PageNotFoundException($"Not found document with name equal {pageName}");
            document.IsOpen = openPage;
            if (string.IsNullOrEmpty(document.Path)) HtmlPageContent = null;
            else
            {
                try
                {
                    HtmlPageContent = System.IO.File.ReadAllText(Path.Combine(documentsSchemeService.GetDocumentsSchemeFolder(), document.Path));
                }
                catch (Exception e)
                {
                    HtmlPageContent =
                        $"Файл по адресу {Path.Combine(documentsSchemeService.GetDocumentsSchemeFolder(), document.Path)} не найден!";
                }
            }

            //HtmlPageContent = !string.IsNullOrEmpty(document.Path)
            //                      ? System.IO.File.ReadAllText(
            //                          Path.Combine(documentsSchemeService.GetDocumentsSchemeFolder(), document.Path))
            //                      : null;
            ViewData[nameof(DocumentsScheme)] = DocumentsScheme;
            SearchText = Title.SearchText;
        }
    }
}
