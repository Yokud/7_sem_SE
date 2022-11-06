namespace WebAPI.Models
{
    public abstract class EntityObjectDTO<T>
    {
        public abstract T GetEntity();
    }
}
