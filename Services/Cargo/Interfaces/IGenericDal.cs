public interface IGenericDal<T> where T : class
{
    void Insert(T obj);
    void Update(T obj);
    void Delete(Guid id);
    T GetById(Guid id);
    List<T> GetAll();
}