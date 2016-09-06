namespace SEPInstance.Dto.InputDto.Users
{
    /// <summary>
    /// 用户名及邮箱重复验证
    /// </summary>
    public class UserValidate
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmailAddress { get; set; }
    }
}