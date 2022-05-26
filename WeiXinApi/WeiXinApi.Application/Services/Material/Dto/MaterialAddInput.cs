

namespace WeiXinApi.Application.Services
{
    public class MaterialAddInput
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        public UploadMediaFileType? UploadMediaFileType { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required(ErrorMessage = "文件不存在")]
        public IFormFile File { get; set; }
    }
}
