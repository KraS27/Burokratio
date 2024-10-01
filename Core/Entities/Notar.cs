using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public string Name { get;  } = null!;

        public Address Location { get;  } = null!;
        
        public Coordinates Coordinates { get; } = null!;

        public 
    }
}
