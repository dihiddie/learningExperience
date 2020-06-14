using System;

namespace LearningExperience.Core.Exceptions
{
    public sealed class PageNotFoundException : ApplicationException
    {
        public PageNotFoundException(string message) : base(message)
        {
        }
    }
}
