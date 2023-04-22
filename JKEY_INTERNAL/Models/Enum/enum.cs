namespace JKEY_INTERNAL.Models.Enum
{
    /// <summary>
    /// enum gender
    /// </summary>
    public enum gender
    {
        /// <summary>
        /// Giới tính nam
        /// </summary>
        Male = 0,

        /// <summary>
        /// Giới tính nữ
        /// </summary>
        Femail = 1,

        /// <summary>
        /// Giới tính khác
        /// </summary>
        Other = 2,
    }

    /// <summary>
    /// enum Active
    /// </summary>
    public enum Active
    {
        /// <summary>
        /// Trạng thái đang hoạt động
        /// </summary>
        Active = 0,

        /// <summary>
        /// trạng thái ngừng hoạt động
        /// </summary>
        InActive = 1,
    }

    /// <summary>
    /// status 
    /// </summary>
    public enum Status
    {

        /// <summary>
        /// đang hoạt động 
        /// </summary>
        Active = 0,

        /// <summary>
        /// dừng hoạt động
        /// </summary>
        InActive = 1,

        /// <summary>
        /// đã được chấp thuâj
        /// </summary>
        Approved = 2,

        /// <summary>
        /// hủy bỏ/ từ chối
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// đã khóa
        /// </summary>
        Blocked = 4,

        /// <summary>
        ///  đang đợi
        /// </summary>
        Pending = 5
    };
}
