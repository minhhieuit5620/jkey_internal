namespace JKEY_INTERNAL.Models.Attributes
{
    /// <summary>
    /// Atribute dùng dể xác định 1 prop là khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Primarykey : Attribute
    {
       
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAttribute : Attribute
    {
        #region Field
        /// <summary>
        /// MSG lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;
        #endregion

        public IsNotNullOrEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
