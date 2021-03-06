using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy.Configuration;

namespace TestStack.BDDfy
{
    public class Scenario
    {
        public Scenario(object testObject, List<Step> steps, string scenarioText, List<string> tags)
        {
            TestObject = testObject;
            Steps = steps;
            Title = scenarioText;
            Tags = tags;
            Id = Configurator.IdGenerator.GetScenarioId();
        }

        public Scenario(string id, object testObject, List<Step> steps, string scenarioText, Example example, List<string> tags)
        {
            Id = id;
            TestObject = testObject;
            Steps = steps;
            Title = scenarioText;
            Example = example;
            Tags = tags;
        }

        public string Id { get; set; }
        public string Title { get; private set; }
        public List<string> Tags { get; private set; }
        public Example Example { get; set; }
        public TimeSpan Duration { get { return new TimeSpan(Steps.Sum(x => x.Duration.Ticks)); } }
        public object TestObject { get; internal set; }
        public List<Step> Steps { get; private set; }

        public Result Result
        {
            get
            {
                if (!Steps.Any())
                    return Result.NotExecuted;

                return (Result)Steps.Max(s => (int)s.Result);
            }
        }
    }
}