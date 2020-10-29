namespace LearningExperience.TopicCreator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите темы через '-'");
                var topics = Console.ReadLine();
                var splittedTopics = topics?.Split('-').Where(x => !string.IsNullOrEmpty(x));
                var compinedTopics = new List<string>();

                int iterator = 0;
                foreach (var splittedTopic in splittedTopics)
                {
                    var combined = $"{{\"Name\":\"{splittedTopic.Trim()}\",\r\n                           \"Value\":\"{splittedTopic.Trim()}\",\r\n                           \"Path\":\"About\\\\About.html\",\r\n                           \"Index\":{iterator + 1},\r\n                           \"Documents\":[]";
                    compinedTopics.Add(combined);
                    iterator++;
                }

                var joined = string.Join("},", compinedTopics);
                Console.WriteLine($"{joined}}}");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так, попробуем снова? y/n");
                var result = Console.ReadLine();
                if (result != null && result.ToLower() == "y") Main(null);
            }

            Main(null);
        }
    }
}
