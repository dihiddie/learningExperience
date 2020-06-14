namespace LearningExperience.Core.Interfaces
{
    using LearningExperience.Core.Models;

    public interface IDocumentsSchemeService
    {
        string GetDocumentsSchemeFolder();

        DocumentsScheme GetScheme();
    }
}
