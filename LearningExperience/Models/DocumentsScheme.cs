using System.Collections.Generic;

namespace LearningExperience.Core.Models
{
    public sealed class DocumentsScheme
    {
        public string Version { get; set; }

        // ReSharper disable once MemberCanBeInternal
        public List<Document> Documents { get; set; }

        // ReSharper disable once ClassNeverInstantiated.Global
        public sealed class Document
        {
            public string Name { get; set; }

            public string Value { get; set; }

            public string Path { get; set; }

            public bool IsOpen { get; set; }

            public int Index { get; set; }

            public List<Document> Documents { get; set; }
        }
    }
}
