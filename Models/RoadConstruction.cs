namespace roadconstruction_apis.Models
{
    public class RoadConstruction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Data> Datas { get; set; }
    }

    public class Data
    {
        public string SamplingTime { get; set; }
        public List<Property> Properties { get; set; }
    }

    public class Property
    {
        public string Label { get; set; }
        public object Value { get; set; }
    }
}
