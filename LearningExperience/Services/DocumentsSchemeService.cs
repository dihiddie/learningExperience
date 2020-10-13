namespace LearningExperience.Core.Services
{
    using System;
    using System.IO;
    using System.Threading;

    using LearningExperience.Core.Interfaces;
    using LearningExperience.Core.Models;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Primitives;

    using Newtonsoft.Json;

    public sealed class DocumentsSchemeService : IDocumentsSchemeService, IDisposable
    {
        private const string WatchFileName = "base.json";

        private readonly PhysicalFileProvider fileProvider;

        private readonly int maxFileReadAttemptsCount;

        private readonly string documentsSchemeFolder;

        public DocumentsSchemeService(IConfiguration configuration)
        {
            documentsSchemeFolder = configuration["DocumentsSchemeFolder"]
                                        ?? throw new NullReferenceException(
                                            "Configuration key - DocumentsSchemeFolder doesn't exist");

            int.TryParse(configuration["MaxFileReadAttemptsCount"], out maxFileReadAttemptsCount);
            if (maxFileReadAttemptsCount == 0) maxFileReadAttemptsCount = 5;

            if (!File.Exists(Path.Combine(documentsSchemeFolder, WatchFileName)))
                throw new FileNotFoundException("Document scheme file not found");

            fileProvider = new PhysicalFileProvider(documentsSchemeFolder);
            GetDocumentScheme();

            SetFoldersToLevelFour();
        }

        private int FileReadAttemptsCount { get; set; }

        private DocumentsScheme DocumentScheme { get; set; }

        private IDisposable TokenCallback { get; set; }

        public string GetDocumentsSchemeFolder() => fileProvider.Root;

        public DocumentsScheme GetScheme() => DocumentScheme;

        public DocumentsScheme GetRenewedScheme()
        {
            DocumentScheme = GetDocumentsScheme();
            SetFoldersToLevelFour();
            return DocumentScheme;
        }

        public void Dispose()
        {
            fileProvider?.Dispose();
        }

        private void WatchForFile()
        {
            IChangeToken changeToken = fileProvider.Watch(WatchFileName);
            TokenCallback = changeToken.RegisterChangeCallback(w => GetDocumentScheme(), null);
        }

        private void GetDocumentScheme()
        {
            try
            {
                Thread.Sleep(10);
                DocumentScheme = GetDocumentsScheme();
                TokenCallback?.Dispose();
                WatchForFile();
                FileReadAttemptsCount = 0;
            }
            catch (IOException)
            {
                if (FileReadAttemptsCount <= maxFileReadAttemptsCount)
                {
                    FileReadAttemptsCount++;
                    GetDocumentScheme();
                }
            }
        }

        private DocumentsScheme GetDocumentsScheme()
        {
            var fileContent = File.ReadAllText(fileProvider.GetFileInfo(WatchFileName).PhysicalPath);
            return JsonConvert.DeserializeObject<DocumentsScheme>(fileContent);
        }

        private void CreateFolders()
        {
            foreach (var documentLevel1 in DocumentScheme.Documents)
            {
                var pathLevel1 = documentLevel1.Value;
                CreateFolderIfNotExists(pathLevel1);
                documentLevel1.Path = null;

                foreach (var documentLevel2 in documentLevel1.Documents)
                {
                    var pathLevel2 = Path.Combine(documentLevel1.Value, documentLevel2.Value);
                    CreateFolderIfNotExists(pathLevel2);
                    documentLevel2.Path = null;

                    foreach (var documentLevel3 in documentLevel2.Documents)
                    {
                        var pathLevel3 = Path.Combine(
                            documentLevel1.Value,
                            documentLevel2.Value,
                            documentLevel3.Value);
                        CreateFolderIfNotExists(pathLevel3);
                        documentLevel3.Path = null;

                        foreach (var documentLevel4 in documentLevel3.Documents)
                        {
                            var folder = documentsSchemeFolder;
                            var pathLevel4 = Path.Combine(
                                folder,
                                documentLevel1.Value,
                                documentLevel2.Value,
                                documentLevel3.Value,
                                $"{documentLevel4.Value}.html");
                            File.Create(pathLevel4);
                            documentLevel4.Path = pathLevel4;
                        }
                    }
                }
            }
        }

        private void SetFoldersToLevelFour()
        {
            foreach (var documentLevel1 in DocumentScheme.Documents)
            {
                documentLevel1.Path = null;

                foreach (var documentLevel2 in documentLevel1.Documents)
                {
                    documentLevel2.Path = null;

                    foreach (var documentLevel3 in documentLevel2.Documents)
                    {
                        documentLevel3.Path = null;

                        foreach (var documentLevel4 in documentLevel3.Documents)
                        {
                            var folder = documentsSchemeFolder;
                            var pathLevel4 = Path.Combine(
                                folder,
                                documentLevel1.Value,
                                documentLevel2.Value,
                                documentLevel3.Value,
                                $"{documentLevel4.Value}.html");
                            documentLevel4.Path = pathLevel4;
                        }
                    }
                }
            }
        }

        private void CreateFolderIfNotExists(string folderName)
        {
            var folder = documentsSchemeFolder;
            if (!Directory.Exists(Path.Combine(folder, folderName)))
                Directory.CreateDirectory(Path.Combine(folder, folderName));
        }
    }
}