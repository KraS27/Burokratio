using Core.Primitives;
using Core.ValueObjects;

namespace Core.Entities
{
    public class Notar : Entity
    {
        public string Name { get; private set; } = null!;

        public Location Location { get; private set; } = null!;
        

    }
}
