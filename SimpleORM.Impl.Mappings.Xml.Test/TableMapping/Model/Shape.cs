namespace SimpleORM.Impl.Mappings.Xml.Test.TableMapping.Model
{
    public class Shape
    {
        public long Id { get; set; }
    }

    public class TwoDimensionalShape : Shape
    {
        public long X { get; set; }
        
        public long Y { get; set; }
    }

    public class Recangle : TwoDimensionalShape
    {
        public long Width { get; set; }
        
        public long Height { get; set; }
    }
}
