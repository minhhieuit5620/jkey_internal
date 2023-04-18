namespace JKEY_INTERNAL.Models.CRUD_Model
{
    public class CrudModel<T> where T : class
    {
        //public string action { get; set; }
        //public object key { get; set; }
        //public string antiForgery { get; set; }
        public T? Value { get; set; }
    }
}
