namespace Src.state
{
    public interface IReadOnlyPopulationState
    {
        int StartPopulation { get; }
        int Population { get; }
        int TotalCases { get; }
        int TotalCasesPeopleWhoIsolated { get; }
        int TotalRecovered { get; }
        int TotalDeaths { get; }
        float TotalDuration { get; }
    }

    public class PopulationState : IReadOnlyPopulationState
    {
        public int StartPopulation { get; set; }
        public int Population { get; set; }
        public int TotalCases { get; set; }
        public int TotalCasesPeopleWhoIsolated { get; set; }
        public int TotalRecovered { get; set; }
        public int TotalDeaths { get; set; }
        public float TotalDuration { get; set; }
    }
}