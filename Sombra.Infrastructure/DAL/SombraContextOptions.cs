namespace Sombra.Infrastructure.DAL
{
    public class SombraContextOptions
    {
        public bool Seed { get; private set; }

        public SombraContextOptions UseSeed()
        {
            Seed = true;
            return this;
        }
    }
}