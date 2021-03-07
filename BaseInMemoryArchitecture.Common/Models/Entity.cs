namespace BaseInMemoryArchitecture.Common.Models
{
    public abstract class Entity
    {
        public abstract int GetId();
        public abstract void SetId(int entityId);
    }
}
